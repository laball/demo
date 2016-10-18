using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    class Program
    {

        private static readonly teseEntities db = new teseEntities();

        static void Main(string[] args)
        {

            //IniteClass();

            //IniteTeacher();

            var teachers = db.Teacher.Where(c => c.Class.Count > 0).ToList();



            var items = new teseEntities().STATDAILY.First();

            Console.ReadLine();
        }


        private static void IniteClass()
        {
            var @class1 = new Class()
            {
                Name = "语文"
            };

            var @class2 = new Class()
            {
                Name = "数学"
            };

            var @class3 = new Class()
            {
                Name = "英语"
            };

            db.Class.AddRange(new Class[] { class1, class2, class3 });
            db.SaveChanges();
        }

        private static void IniteTeacher()
        {
            var teacher1 = new Teacher
            {
                Name = "黄庆"
            };

            var teacher2 = new Teacher
            {
                Name = "邓起渊"
            };

            var teacher3 = new Teacher
            {
                Name = "杨宝军"
            };

            db.Teacher.AddRange(new Teacher[] { teacher1, teacher2, teacher3 });
            db.SaveChanges();

        }


    }
}
