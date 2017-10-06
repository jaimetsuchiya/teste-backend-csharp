using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TorreHanoi.ServiceAgent
{
    public class SlackServiceAgent : ISlackServiceAgent
    {
        private readonly HttpClient _client;
        private readonly string _apiRoute;

        public SlackServiceAgent(string endereco, string route)
        {
            _client = new HttpClient {BaseAddress = new Uri(endereco)};
            _apiRoute = route;
        }

        public async Task<bool> Post(string mensgem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { text = mensgem }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_apiRoute, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString.Equals("ok");
        }
    }
}
