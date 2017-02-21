using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreConsole
{
    public class EFCoreSQLServerTest
    {
        public static void Test()
        {
            using (var db = new TestContext())
            {
                Student newStudent = db.Student.Where(c => c.Name == "Lee").FirstOrDefault();
                if (newStudent == null)
                {
                    newStudent = new Student
                    {
                        Name = "Lee",
                        ClassLevel = null,
                        Age = 18,
                        BirthDay = DateTime.Now
                    };

                    db.Student.Add(newStudent);
                }
                else
                {
                    newStudent.Age += 1;
                    newStudent.BirthDay = DateTime.Now;
                }

                db.SaveChanges();

                var students = db.Student;
            }
        }
    }

    public class TestContext : DbContext
    {
        public DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(MSSQLServerTest.ConnectionString);
        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ClassLevel { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
    }

    public class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


}
