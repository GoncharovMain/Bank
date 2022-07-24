using System.Text.Json;
using System.Text;
using Bank.Models;

#nullable disable

namespace Store
{
    public class StoreClient
    {
        private string _origin;
        private HttpClient _client;
        public StoreClient()
        {
            _origin = "https://localhost:7168/";

            HttpClientHandler clientHandler = new HttpClientHandler();

            clientHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

            _client = new HttpClient(clientHandler);
        }

        public void CreateOrderPost(Order order)
        {
            string url = _origin + "api/order/create-order";

            HttpResponseMessage response = _client
                .PostAsync(url, GetJsonContent<Order>(order)).Result;

            string result = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine($"Process: {result}");
        }

        public void GetStatusPost(int id)
        {
            string url = _origin + $"api/order/get-status/{id}";

            HttpResponseMessage response = _client.GetAsync(url).Result;

            string status = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine($"{status} for id: {id}");
        }

        private HttpContent GetJsonContent<T>(T obj)
        {
            string json = JsonSerializer.Serialize(obj);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}