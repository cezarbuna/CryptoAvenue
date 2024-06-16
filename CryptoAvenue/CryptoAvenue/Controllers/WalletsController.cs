using AutoMapper;
using CryptoAvenue.Application.UserApp.UserCommands;
using CryptoAvenue.Application.UserApp.UserQuery;
using CryptoAvenue.Application.WalletApp.WalletCommands;
using CryptoAvenue.Application.WalletApp.WalletQueries;
using CryptoAvenue.Dal.Dtos.UserDtos;
using CryptoAvenue.Dtos.WalletDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public WalletsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] WalletPostDto walletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateWalletCommand
            {
                UserId = walletDto.UserId,
                Quantity = walletDto.Quantity
            };

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetWalletById), new { walletId = result.Id }, result);
        }
        [HttpPatch]
        [Route("withdraw/{userId}/{quantity}")]
        public async Task<IActionResult> Withdraw( Guid userId, double quantity)
        {

            var command = new WithdrawCommand
            {
                UserId = userId,
                Quantity = quantity
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPost]
        [Route("trade/{userId}/{sourceCoinId}/{sourceQuantity}/{targetCoinId}/{targetQuantity}")]
        public async Task<IActionResult> Trade(Guid userId, string sourceCoinId, double sourceQuantity, string targetCoinId, double targetQuantity)
        {
            var command = new TradeCommand
            {
                UserId = userId,
                SourceCoinId = sourceCoinId,
                SourceQuantity = sourceQuantity,
                TargetCoinId = targetCoinId,
                TargetQuantity = targetQuantity
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpGet]
        [Route("get-wallet-by-id/{walletId}")]
        public async Task<IActionResult> GetWalletById(Guid walletId)
        {
            var query = new GetWalletByIdQuery { WalletId = walletId };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet]
        [Route("get-wallet-by-user-id/{userId}")]
        public async Task<IActionResult> GetWalletByUserId(Guid userId)
        {
            var query = new GetWalletByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet]
        [Route("get-portfolio-information/{walletId}")]
        public async Task<IActionResult> GetPortfolioInformation(Guid walletId)
        {
            var query = new GetPortfolioInformationQuery { WalletId = walletId };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet]
        [Route("get-portfolio-history/{walletId}/{days}")]
        public async Task<IActionResult> GetPortfolioHistory(Guid walletId, int days)
        {
            var query = new GetPortfolioHistoryQuery { WalletId = walletId, Days = days };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet]
        [Route("get-portfolio-change-24h/{walletId}")]
        public async Task<IActionResult> GetPortfolioChange24h(Guid walletId)
        {
            var query = new GetPortfolioChange24hQuery { WalletId = walletId };
            var result = await _mediator.Send(query);
            return Ok(Math.Round(result,3));
        }
    }
}
