using MongoDB.Bson;
using MongoDB.Driver;

namespace ServiceTwo
{
    public interface IWalletService
    {
        Task<List<Wallet>> GetWalletsAsync();

        Task<Wallet> GetWalletByIdAsync(ObjectId id);

        Task<bool> UpdateWalletAsync(Wallet wallet);
    }

    public class WalletService : IWalletService
    {
        private IConfiguration _configuration;
        private readonly IMongoCollection<Wallet> _walletCtx;

        public WalletService(IConfiguration configuration)
        {
            _configuration = configuration;
            var _mongoClient = new MongoClient(_configuration["MongoDB:ConnectionString"]);
            var _mongoDb = _mongoClient.GetDatabase(_configuration["MongoDB:DatabaseName"]);
            _walletCtx = _mongoDb.GetCollection<Wallet>(_configuration["MongoDB:CollectionName"]);
        }

        public async Task<List<Wallet>> GetWalletsAsync()
        {
            var response = await _walletCtx.FindAsync(w => true);
            return response.ToList();
        }

        public async Task<Wallet> GetWalletByIdAsync(ObjectId id)
        {
            var response = await _walletCtx.FindAsync(w => w.Id == id);
            return response.First();
        }

        public async Task<bool> UpdateWalletAsync(Wallet wallet)
        {
            var result = await _walletCtx.ReplaceOneAsync(w => w.Id == wallet.Id, wallet);
            return result.IsAcknowledged;
        }
    }
}
