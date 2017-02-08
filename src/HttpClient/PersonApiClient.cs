using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.HttpClient
{
    public class PersonApiClient 
    {
        private readonly System.Net.Http.HttpClient _client;
        
        public PersonApiClient(System.Net.Http.HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic","password");
        }

        public async Task<HttpResponseMessage> GetAllPersons()
        {
            return await _client.GetAsync("person");
        }

        public async Task<HttpResponseMessage> GetPersonById(string id)
        {
            return  await _client.GetAsync($"person/{id}");
        }

        public async Task<HttpResponseMessage> CreatePerson(string name)
        {
            return await _client.PostAsync("person", name.Serialize());
        }

        public async Task<HttpResponseMessage> Update(string id, string name)
        {
            return await _client.PutAsync($"person/{id}", name.Serialize());
        }

        public async Task<HttpResponseMessage> DeletePerson(string id)
        {
            return await _client.DeleteAsync($"person/{id}");
        }
    }
}
