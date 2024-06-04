using AutoMapper;
using CryptoAvenue.Application.TransactionApp.TransactionQueries;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.TransactionDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public TransactionsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-transactions-by-user-id/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(Guid userId)
        {
            var query = new GetTransactionsByUserIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            var mapppedResult = _mapper.Map<List<TransactionGetDto>>(result);
            return Ok(mapppedResult);
        }
    }
}
