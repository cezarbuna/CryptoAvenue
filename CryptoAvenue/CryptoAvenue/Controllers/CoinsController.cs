using AutoMapper;
using CryptoAvenue.Application;
using CryptoAvenue.Application.UserApp.UserQuery;
using CryptoAvenue.Dal.Dtos.UserDtos;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.CoinDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        private readonly CryptoAvenueDbContext _dbContext;

        public CoinsController(IMapper mapper, IMediator mediator, CryptoAvenueDbContext dbContext)
        {
            _mapper = mapper;
            _mediator = mediator;
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("get-all-coins")]
        public async Task<IActionResult> GetAllCoins ()
        {
            var coins = await _dbContext.Coins.ToListAsync();
            //var mappedCoins = _mapper.Map<CoinGetDto>(coins);
            return Ok(coins);
        }
        [HttpGet]
        [Route("get-coin-by-id/{coinId}")]
        public async Task<IActionResult> GetCoinById (string coinId)
        {
            var coins = await _dbContext.Coins.SingleOrDefaultAsync(x => x.Id == coinId);
            return Ok(coins);
        }
        [HttpGet]
        [Route("get-user-coins/{userId}")]
        public async Task<IActionResult> GetUserCoins(Guid userId)
        {
            var wallet = await _dbContext.Wallets.SingleOrDefaultAsync(x => x.UserId == userId);
            var coins = new List<Coin> ();
            if(wallet != null)
            {
                var coinWallets = await _dbContext.WalletCoins.Where(x => x.WalletId == wallet.Id).ToListAsync();
                foreach(var coinWallet in coinWallets)
                {
                    var coin = await _dbContext.Coins.FirstOrDefaultAsync(x => x.Id == coinWallet.CoinId);
                    coins.Add(coin);
                }
            }
            return Ok(coins);
        }
        [HttpGet]
        [Route("get-available-quantity/{coinId}/{userId}")]
        public async Task<IActionResult> GetAvailableQuantity(string coinId, Guid userId)
        {
            var wallet = await _dbContext.Wallets.SingleOrDefaultAsync(x => x.UserId==userId);
            var quantity =  _dbContext.WalletCoins.SingleOrDefault(x => x.WalletId == wallet.Id && x.CoinId == coinId).Quantity;
            return Ok(quantity);
        }
    }
}
