using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.HttpClient
{
    public static class PersonApiExtension
    {
        public static async Task<T> ConvertToObject<T>(this HttpContent content)
        {
            var result = await content.ReadAsStringAsync();
            var @object = JsonConvert.DeserializeObject<T>(result);
            return @object;
        }

        public static StringContent Serialize(this object @object)
        {
            var content = JsonConvert.SerializeObject(@object);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}