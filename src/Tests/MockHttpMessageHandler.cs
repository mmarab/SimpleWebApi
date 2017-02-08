using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using Newtonsoft.Json;

namespace UnitTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<HttpRequestMessage, HttpResponseMessage> _responses = new Dictionary<HttpRequestMessage, HttpResponseMessage>();

        public MockHttpMessageHandler()
        {
            _responses.Add(new HttpRequestMessage(HttpMethod.Post, "http://test.url/api/person"), new HttpResponseMessage(HttpStatusCode.Created) {Content = new StringContent("Resource Created"), Headers = { Location = new Uri("http://test.url/api/person/123-123-123")}});
            _responses.Add(new HttpRequestMessage(HttpMethod.Get, "http://test.url/api/person/123"), new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(SerializeObject(new Person("Bruce Wayne", Guid.NewGuid())))});
            _responses.Add(new HttpRequestMessage(HttpMethod.Get, "http://test.url/api/person"), new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(SerializeObject(new [] { new Person("Vince Van", Guid.NewGuid()), new Person("Peter Parker", Guid.NewGuid()) })) });
            _responses.Add(new HttpRequestMessage(HttpMethod.Put, "http://test.url/api/person/123"), new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("") });
            _responses.Add(new HttpRequestMessage(HttpMethod.Delete, "http://test.url/api/person/123"), new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("") });
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = _responses.First(f => f.Key.Method == request.Method 
            && string.Compare(f.Key.RequestUri.ToString(), request.RequestUri.ToString(), StringComparison.Ordinal) ==0).Value;
           return await Task.FromResult(response);
        }

        public static string SerializeObject(object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}