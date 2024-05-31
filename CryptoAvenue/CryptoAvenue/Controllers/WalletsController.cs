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
    }
}
