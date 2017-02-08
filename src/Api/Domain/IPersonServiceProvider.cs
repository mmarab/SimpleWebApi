using System;
using System.Collections.Generic;

namespace API.Domain
{
    public interface IPersonRepository
    {
        Person Get(Guid id);
        void Delete(Guid id);
        void Create(Person person);
        void Update(Guid id, string name);
        IEnumerable<Person> Get();
    }
}
