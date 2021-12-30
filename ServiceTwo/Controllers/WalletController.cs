using Microsoft.AspNetCore.Mvc;
using SagaCommon;

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
        public async Task<Wallet> Get(int id)
        {
            var response = await _walletService.GetWalletsAsync();

            return await _walletService.GetWalletByIdAsync(response[0].Id);
        }

        // POST api/<WalletController>
        [HttpPost]
        public async void Post([FromBody] long value)
        {
            var response = await _walletService.GetWalletsAsync();

            Wallet w = new Wallet
            {
                Id = response[0].Id,
                Amount = response[0].Amount + value
            };

            await _walletService.UpdateWalletAsync(response[0].Id, w);
        }

        // PUT api/<WalletController>/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] string value)
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
