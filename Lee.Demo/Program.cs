using System;
using System.Data.Entity;
using System.Linq;
using Lee.Entities;
using Lee.EntityFramework;
using Lee.PostgreSQL.Entities;
using Lee.PostgreSQL.EntityFramework;

namespace Lee.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
           

            using (var context = new LeeDbContext())
            {
                //var item = context.Set<OptimisticLockTable>().FirstOrDefault();
                //item.Name = "Lee" + new Random().Next(100,999);
                var ddd = context.Set<MasterTable>()
                    //.Include("Items")
                    .Include(c => c.Items)
                    .ToList();





                //var master = new MasterTable
                //{
                //    Name = "master1",
                //    Code = "master1"                
                //};

                //var items1 = new ItemTable
                //{
                //    Description = "Des1"
                //};

                //var items2 = new ItemTable
                //{
                //    Description = "Des1"
                //};

                //master.Items.Add(items1);
                //master.Items.Add(items2);

                //context.Set<MasterTable>().Add(master);

                //context.SaveChanges();
            }

            var a = 1 + 2;


            //OptimisticLockTable

            Console.ReadLine();
        }
    }
}
