using System;
using System.Data.Entity;
using System.Linq;
using Lee.PostgreSQL.Entities;
using Lee.PostgreSQL.EntityFramework;

namespace Lee.PostgreSQL.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryData();

            Console.ReadLine();
        }

        static void QueryData()
        {
            using (var pgdb = new NpgsqlDbContext())
            {
                var nameClause = $"%L%";
                var students = pgdb.Students.Where(c => DbFunctions.Like(c.Name, nameClause)).ToList();
                if(students.Any())
                {
                    Console.WriteLine("get data success");
                }
            }
        }

        static void AddData()
        {
            using (var pgdb = new NpgsqlDbContext())
            {

                var students = new[] {
                     new Student
                {
                    Name = "Lee",
                    Code = "125"
                },
                    new Student
                {
                    Name = "Mao",
                    Code = "777"
                },
                    new Student
                {
                    Name = "Liu",
                    Code = "345"
                }
                };


                pgdb.Students.AddRange(students);

                pgdb.SaveChanges();
            }
        }

    }
}
