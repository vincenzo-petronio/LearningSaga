using MongoDB.Bson;
using MongoDB.Driver;
using SagaCommon;

namespace ServiceOne.Data
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        Task<TPrimaryKey> InsertAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);
    }

    public interface IProductRepository : IRepository<Product, ObjectId>
    {

    }

    public class MongoRepository : IDisposable
    {
        private IConfiguration _configuration;
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDb;

        public MongoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["MongoDB:ConnectionString"]);
            _mongoDb = _mongoClient.GetDatabase(_configuration["MongoDB:DatabaseName"]);
        }

        public void Dispose()
        {
            _mongoClient = null;
            _mongoDb = null;
        }

        public IMongoDatabase Database
        {
            get { return _mongoDb; }
        }
    }


    public class ProductRepository : MongoRepository, IProductRepository
    {
        private IConfiguration _configuration;
        private readonly IMongoCollection<Product> _productsCtx;

        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _productsCtx = Database.GetCollection<Product>(_configuration["MongoDB:CollectionName"]);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var response = await _productsCtx.FindAsync(f => true);
            return response.ToList();
        }

        public async Task<Product> GetByIdAsync(ObjectId id)
        {
            var response = await _productsCtx.FindAsync(o => o.Id == id);
            return response.First();
        }

        public Task<ObjectId> InsertAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Product entity)
        {
            var result = await _productsCtx.ReplaceOneAsync(o => o.Name == entity.Name, entity);
            return result.UpsertedId != null;
        }

        public Task<bool> DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
