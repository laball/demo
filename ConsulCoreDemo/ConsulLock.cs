using System;
using System.Collections.Generic;
using System.Text;
using Consul;

namespace ConsulCoreDemo
{


    /// <summary>
    /// http://zhuanlan.51cto.com/art/201704/536943.htm
    /// 
    /// 由于Java版本的Consul的API与.net版本差异较大，尚未来得及校验；
    /// 
    /// </summary>
    public class ConsulLock
    {
        private const String prefix = "lock/";  // 同步锁参数前缀

        private ConsulClient consulClient;
        private String sessionName;
        private String sessionId = null;
        private String lockKey;

        /**
         *
         * @param consulClient
         * @param sessionName   同步锁的session名称
         * @param lockKey       同步锁在consul的KV存储中的Key路径，会自动增加prefix前缀，方便归类查询
         */
        public ConsulLock(ConsulClient consulClient, String sessionName, String lockKey)
        {
            this.consulClient = consulClient;
            this.sessionName = sessionName;
            this.lockKey = prefix + lockKey;
        }

        /**
         * 获取同步锁
         *
         * @param block     是否阻塞，直到获取到锁为止
         * @return
         */
        public Boolean Lock(bool block)
        {
            /*
            if (sessionId != null)
            {
                throw new Exception(sessionId + " - Already locked!");
            }

            sessionId = CreateSession(sessionName);
            while (true)
            {

                var kvPair = new KVPair(lockKey);
                kvPair.Session = sessionId;

                consulClient.KV.Put(kvPair).Sync();


                PutParams putParams = new PutParams();
                putParams.setAcquireSession(sessionId);

                //var dd = new KVPair(sessionId);


                if (consulClient.setKVValue(lockKey, "lock:" + LocalDateTime.now(), putParams).getValue())
                {
                    return true;
                }
                else if (block)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }*/


            throw new NotImplementedException();
            
        }

        /**
         * 释放同步锁
         *
         * @return
         */
        public Boolean unlock()
        {
            var result = consulClient.Session.Destroy(sessionId).Sync();
            return result.Response;

            //PutParams putParams = new PutParams();
            //putParams.setReleaseSession(sessionId);
            //boolean result = consulClient.setKVValue(lockKey, "unlock:" + LocalDateTime.now(), putParams).getValue();
            //consulClient.sessionDestroy(sessionId, null);
            //return result;
        }

        /**
         * 创建session
         * @param sessionName
         * @return
         */
        private string CreateSession(string sessionId)
        {
            var entry = new SessionEntry();
            entry.ID = sessionId;

            var result = consulClient.Session.Create(entry).Sync();
            return result.Response;

            //NewSession newSession = new NewSession();
            //newSession.setName(sessionName);
            //return consulClient.sessionCreate(newSession, null).getValue();
        }
    }

}
