using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Results;
using API.Controllers;
using API.Domain;
using API.Infrastructure;
using NSubstitute;
using Xunit;

namespace UnitTests
{
    public class ApiTests
    {
        [Fact]
        public void PersonController_Get_ShouldReturnOkWithEnumerableContent()
        {
            //Setup
            var id = Guid.NewGuid();
            var repo = Substitute.For<IPersonRepository>();
            repo.Get().Returns(new[] { new Person("marcello", id) });
            var controller = new PersonController(repo);

            //Action
            var actionResult = controller.Get();

            //Result
            var resultContent = Assert.IsType<OkNegotiatedContentResult<IEnumerable<Person>>>(actionResult);
            Assert.Equal(1, resultContent.Content.ToList().Count);
            Assert.Equal(id, resultContent.Content.ToList()[0].Id);
            Assert.Equal("marcello", resultContent.Content.ToList()[0].Name);
        }

        [Fact]
        public void PersonController_Get_ShouldReturnOkWithPerson()
        {
            //Setup
            var id = Guid.NewGuid();
            var repo = Substitute.For<IPersonRepository>();
            repo.Get(id).ReturnsForAnyArgs(new Person("bruce", id));
            var controller = new PersonController(repo);

            //Action
            var actionResult = controller.Get(id.ToString());

            //Result
            var resultContent = Assert.IsType<OkNegotiatedContentResult<Person>>(actionResult);
            Assert.Equal(id, resultContent.Content.Id);
            Assert.Equal("bruce", resultContent.Content.Name);
        }

        [Fact]
        public void PersonController_Post_ShouldCreateResourceAndProvideLocationHeader()
        {
            //Setup
            var controller = new PersonController(PersonRepository.Instance)
            {
                Request = new HttpRequestMessage {RequestUri = new Uri("http://example/api/persons")}
            };

            //Action
            var actionResult = controller.Post("Peter Griffin");

            //Result
            var resultContent = Assert.IsType<CreatedNegotiatedContentResult<string>>(actionResult);
            Assert.Contains(@"http://example/api/persons", resultContent.Location.ToString());
        }

        [Fact]
        public void PersonController_Put_ShouldReturnOk()
        {
            //Setup
            var id = Guid.NewGuid();
            var repo = Substitute.For<IPersonRepository>();
            repo.Get().Returns(new[] { new Person("bruce", id) });
            var controller = new PersonController(repo);

            //Action
            var actionResult = controller.Put(id.ToString(), "yohan");

            //Result
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public void PersonController_Delete_ShouldReturnOk()
        {
            //Setup
            var id = Guid.NewGuid();
            var repo = Substitute.For<IPersonRepository>();
            repo.When(x => x.Delete(id)).DoNotCallBase();
            var controller = new PersonController(repo);

            //Action
            var actionResult = controller.Delete(id.ToString());

            //Result
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
