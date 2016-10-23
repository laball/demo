using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoMapperDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DTO_User, EntityUser>());
        }
    }

    public class DTO_User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? Sex { get; set; }
    }

    public class EntityUser
    {
        public int ID { get; set; }

        public int Age { get; set; }

        public string UserName { get; set; }
    }
}