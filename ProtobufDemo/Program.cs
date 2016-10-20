using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace ProtobufDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person
            {
                Id = 12345,
                Name = "Fred",
                Address = new Address
                {
                    Line1 = "Flat 1",
                    Line2 = "The Meadows"
                }
            };

            using (var file = File.Create("person.bin"))
            {
                Serializer.Serialize(file, person);
            }

            Person newPerson;
            using (var file = File.OpenRead("person.bin"))
            {
                newPerson = Serializer.Deserialize<Person>(file);
            }

        }
    }


    [ProtoContract]
    class Person
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public Address Address { get; set; }
    }

    [ProtoContract]
    class Address
    {
        [ProtoMember(1)]
        public string Line1 { get; set; }
        [ProtoMember(2)]
        public string Line2 { get; set; }
    }

}
