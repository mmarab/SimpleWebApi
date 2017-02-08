using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using API.Domain;
using API.HttpClient;
using Xunit;

namespace UnitTests
{
    public class TestClient
    {
        [Fact]
        public async void PersonApiClient_Create_ShouldReturn201WithLocationHeader()
        {
            //SETUP
            var client = new PersonApiClient(new HttpClient(new MockHttpMessageHandler()) {BaseAddress = new Uri("http://test.url/api/") });

            //ACT
            var result = await client.CreatePerson("Bruce Wayne");


            //RESULT
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.Equal("http://test.url/api/person/123-123-123", result.Headers.Location.ToString());

        }

        [Fact]
        public async void PersonApiClient_GetAllPersons_ShouldReturn200WithEnumerable()
        {
            //SETUP
            var client = new PersonApiClient(new HttpClient(new MockHttpMessageHandler()) { BaseAddress = new Uri("http://test.url/api/") });

            //ACT
            var result = await client.GetAllPersons();
            var persons = await result.Content.ConvertToObject<IEnumerable<Person>>();


            //RESULT
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(2, persons.Count());

        }

        [Fact]
        public async void PersonApiClient_GetPerson_ShouldReturn200WithPerson()
        {
            //SETUP
            var client = new PersonApiClient(new HttpClient(new MockHttpMessageHandler()) { BaseAddress = new Uri("http://test.url/api/") });

            //ACT
            var result = await client.GetPersonById("123");
            var persons = await result.Content.ConvertToObject<Person>();


            //RESULT
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("Bruce Wayne", persons.Name);

        }

        [Fact]
        public async void PersonApiClient_DeletePerson_ShouldReturn200()
        {
            //SETUP
            var client = new PersonApiClient(new HttpClient(new MockHttpMessageHandler()) { BaseAddress = new Uri("http://test.url/api/") });

            //ACT
            var result = await client.DeletePerson("123");
            
            //RESULT
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void PersonApiClient_UpdatePerson_ShouldReturn200()
        {
            //SETUP
            var client = new PersonApiClient(new HttpClient(new MockHttpMessageHandler()) { BaseAddress = new Uri("http://test.url/api/") });

            //ACT
            var result = await client.Update("123","Steve Jobs");

            //RESULT
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
