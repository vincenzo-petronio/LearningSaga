using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceOne.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productService.GetProductsAsync();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<Product> Get(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            return await _productService.GetProductByIdAsync(objectId);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async void Post(Product product)
        {
            var products = await _productService.GetProductsAsync();
            var prod = products.Where(p => p.Name == product.Name).First();
            prod.Quantity -= product.Quantity;

            await _productService.UpdateProductAsync(prod);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
