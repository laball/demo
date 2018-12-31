using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using org.apache.zookeeper;
using static org.apache.zookeeper.ZooDefs;

namespace ZookeeperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var zkLock = new ZooKeeperWriteLock(new ZooKeeperReadWriteLockOptions()))
            //{
            //    zkLock.Lock();

            //    Thread.Sleep(1000);

            //    zkLock.UnLock();
            //}

            ZooKeeperReadWriteLockTest();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static void ZooKeeperReadWriteLockTest()
        {

            var rd = new Random();

            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    var seed = rd.Next(1, 100);
                    IDistributedLock zkLock = ((seed % 2) == 1)
                    ?
                    new ZooKeeperWriteLock(new ZooKeeperReadWriteLockOptions()) as IDistributedLock
                    :
                    new ZooKeeperReadLock(new ZooKeeperReadWriteLockOptions()) as IDistributedLock;

                    using (zkLock)
                    {
                        if (zkLock.Lock())
                        {
                            Thread.Sleep(1000);

                            zkLock.UnLock();
                            Trace.WriteLine("UnLock");
                        }
                        else
                        {
                            Trace.WriteLine("Get Lock Failed.");
                        }
                    }
                });
            }
        }

        static void ZooKeeperLockTest()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    using (var zkLock = new ZooKeeperLock("127.0.0.1:2181", "test"))
                    {
                        if (zkLock.Lock())
                        {
                            Thread.Sleep(1000);

                            zkLock.UnLock();
                            Trace.WriteLine("UnLock");
                        }
                        else
                        {
                            Trace.WriteLine("Get Lock Failed.");
                        }
                    }
                });
            }
        }

        static async Task Test()
        {
            var output = string.Empty;
            ZooKeeper zk = null;
            try
            {
                //创建一个Zookeeper实例，第一个参数为目标服务器地址和端口，第二个参数为Session超时时间，第三个为节点变化时的回调方法 

                ZooKeeper.LogToFile = false;
                ZooKeeper.LogToTrace = true;

                zk = new ZooKeeper("127.0.0.1:2181", 10 * 1000, NullWatcher.Instance);
                var stat = await zk.existsAsync("/root", true);

                if (stat == null)
                {
                    //创建一个节点root，数据是mydata,不进行ACL权限控制，节点为永久性的(即客户端shutdown了也不会消失) 
                    output = await zk.createAsync("/root", "mydata".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                    Trace.WriteLine($"output");
                }

                stat = await zk.existsAsync("/root/childone", true);
                if (stat == null)
                {
                    //在root下面创建一个childone znode,数据为childone,不进行ACL权限控制，节点为永久性的 
                    output = await zk.createAsync("/root/childone", "childone".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                    Trace.WriteLine($"output");
                }

                //取得/root节点下的子节点名称,返回List<String> 
                var subNodes = await zk.getChildrenAsync("/root", true);
                Trace.WriteLine($"SubNodes: {(string.Join(",", subNodes.Children))}");

                //取得/root/childone节点下的数据,返回byte[] 
                var data = await zk.getDataAsync("/root/childone", true);
                Trace.WriteLine($"/root/childone Data: {Encoding.UTF8.GetString(data.Data)}");

                //修改节点/root/childone下的数据，第三个参数为版本，如果是-1，那会无视被修改的数据版本，直接改掉
                await zk.setDataAsync("/root/childone", "childonemodify".GetBytes(), -1);

                //删除/root/childone这个节点，第二个参数为版本，－1的话直接删除，无视版本 
                await zk.deleteAsync("/root/childone", -1);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.StackTrace);
            }
            finally
            {
                await zk.closeAsync();
            }
        }
    }
}
