using System;
using System.Collections.Generic;
using System.Net.Http;
using API.Domain;
using API.HttpClient;
using Nito.AsyncEx;

namespace Smoke.Test.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async void MainAsync(string[] args)
        {
           var client = new PersonApiClient(new HttpClient() {BaseAddress = new Uri("http://localhost:38335/api/")});
           var response = await client.CreatePerson("quagmire");
           var locationHeader = response.Headers.Location;
           var getPerson = await client.GetAllPersons();
           var result = getPerson.Content.ConvertToObject<IEnumerable<Person>>();
            Console.ReadLine();
        }
    }
}
