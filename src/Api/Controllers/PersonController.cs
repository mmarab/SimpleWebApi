using System;
using System.Web.Http;
using API.Domain;
using API.Infrastructure;

namespace API.Controllers
{
    public class PersonController : ApiController
    {
        private static IPersonRepository _repository;

        public PersonController() : this(PersonRepository.Instance) {}


        public PersonController(IPersonRepository repository)
        {
            _repository = repository;
        }
       
        public IHttpActionResult Get()
        {
           return Ok(_repository.Get());
        }
        
        public IHttpActionResult Get(string id)
        {
            return Ok(_repository.Get(Guid.Parse(id)));
        }

        public IHttpActionResult Post([FromBody]string name)
        {
            var id = Guid.NewGuid();
            _repository.Create(new Person(name, id));
            return Created(new Uri($"{Request.RequestUri}/{id}"),"");
        }
        
        public IHttpActionResult Put(string id, [FromBody] string name)
        {
            _repository.Update(Guid.Parse(id), name);
           return Ok();
        }
        
        public IHttpActionResult Delete(string id)
        {
            _repository.Delete(Guid.Parse(id));
            return Ok();
        }
    }
}
