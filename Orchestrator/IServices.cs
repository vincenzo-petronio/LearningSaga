using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Orchestrator
{
    public class Product
    {
        //[JsonPropertyName("_id")]
        //public string Id { get; set; }

        public string Name { get; set; }

        public long Quantity { get; set; }

        public int Price { get; set; }
    }

    public class Wallet
    {
        //[JsonPropertyName("_id")]
        //public string Id { get; set; }

        public long Amount { get; set; }
    }

    public interface IServices
    {
        Task<List<Product>> GetAllProducts();
        Task<bool> OrderProduct(string productName, int items);

        Task<List<Wallet>> GetAllWallets();
        Task SendMoney(long amount);
    }

    public class Services : IServices
    {
        private readonly IHttpClientFactory _clientFactory;

        public Services(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var httpClient = _clientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:6100/api/Products");
            string responseJson = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<Product>>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<bool> OrderProduct(string productName, int items)
        {
            string json = JsonSerializer.Serialize(new { Name = productName, Quantity = items });
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var httpClient = _clientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.PostAsync("http://localhost:6100/api/Products", httpContent);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public Task<List<Wallet>> GetAllWallets()
        {
            throw new NotImplementedException();
        }

        public Task SendMoney(long amount)
        {
            throw new NotImplementedException();
        }
    }
}
