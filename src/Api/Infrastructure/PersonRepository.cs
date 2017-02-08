using System;
using System.Collections.Generic;
using System.Linq;
using API.Domain;

namespace API.Infrastructure
{
    public class PersonRepository : IPersonRepository
    {
        private static PersonRepository _instance;
        private IList<Person> _persons;

        private PersonRepository()
        {
            _persons = new List<Person>();
        }

        public static PersonRepository Instance => _instance ?? (_instance = new PersonRepository());

        public Person Get(Guid id)
        {
            return _persons.First(f => f.Id.CompareTo(id) == 0);
        }

        public void Delete(Guid id)
        {
            var person = Get(id);
            _persons.Remove(person);
        }

        public void Create(Person person)
        {
            _persons.Add(person);
        }

        public void Update(Guid id, string name)
        {
            //lets just cheat here. 
            var newPerson = new Person(name, id);
            Delete(id);
            Create(newPerson);
        }

        public IEnumerable<Person> Get()
        {
            return _persons;
        }
    }
}