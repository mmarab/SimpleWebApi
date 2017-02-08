using System;

namespace API.Domain
{
    public class Person
    {
        public Guid Id { get; }
        public string Name { get; }

        public Person(string name, Guid id)
        {
            Name = name;
            Id = id;
        }
    }
}