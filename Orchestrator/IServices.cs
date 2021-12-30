using SagaCommon;
using System.Net.Http.Json;

namespace Orchestrator
{
    public interface IServices
    {
        Task<List<Product>> GetAllProducts();
        Task<bool> OrderProduct(string name, int quantity);

        Task<List<Wallet>> GetAllWallets();
        Task<bool> SendMoney(long amount);
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

            //string responseJson = await response.Content.ReadAsStringAsync();
            //var result = JsonSerializer.Deserialize<List<Product>>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var result = await response.Content.ReadFromJsonAsync<List<Product>>();
            return result;
        }

        public async Task<bool> OrderProduct(string name, int quantity)
        {
            var httpClient = _clientFactory.CreateClient();

            //string json = JsonSerializer.Serialize(new { Name = product.Name, Quantity = product.Quantity });
            //StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, MediaTypeNames.Application.Json);
            //HttpResponseMessage response = await httpClient.PostAsync("http://localhost:6100/api/Products", httpContent);

            Product p = new Product
            {
                Name = name,
                Quantity = quantity
            };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:6100/api/Products", p);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public Task<List<Wallet>> GetAllWallets()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendMoney(long amount)
        {
            var httpClient = _clientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:6200/api/Wallet", amount);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
