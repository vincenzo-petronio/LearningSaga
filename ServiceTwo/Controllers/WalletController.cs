using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private readonly IWalletService _walletService;

        public WalletController(ILogger<WalletController> logger, IWalletService walletService)
        {
            _logger = logger;
            _walletService = walletService;
        }

        // GET: api/<WalletController>
        [HttpGet]
        public async Task<List<Wallet>> Get()
        {
            var response = await _walletService.GetWalletsAsync();
            return response;
        }

        // GET api/<WalletController>/5
        [HttpGet("{id}")]
        public async Task<Wallet> Get(string id)
        {
            //ObjectId objectId = ObjectId.Parse(id);
            //return await _walletService.GetWalletByIdAsync(objectId);

            var wallets = await _walletService.GetWalletsAsync();
            return wallets.First();
        }

        // POST api/<WalletController>
        [HttpPost]
        public async void Post(Wallet w)
        {
            var wallet = await Get("");
            wallet.Amount = w.Amount;

            await _walletService.UpdateWalletAsync(wallet);
        }

        // PUT api/<WalletController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<WalletController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
