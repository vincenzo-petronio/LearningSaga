using Common;
using MongoDB.Bson;
using ServiceOne.Data;

namespace ServiceOne
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(ObjectId id);

        Task<bool> UpdateProductAsync(Product product);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _productRepository.GetAllAsync();
            return response.ToList();
        }

        public async Task<Product> GetProductByIdAsync(ObjectId id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }
    }
}
