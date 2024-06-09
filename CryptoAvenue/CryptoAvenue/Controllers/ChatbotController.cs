using AutoMapper;
using CryptoAvenue.Application.WalletApp.WalletQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoAvenue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatbotController(IMapper mapper, IMediator mediator, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _mediator = mediator;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        [Route("get-portfolio-info/{userId}")]
        public async Task<IActionResult> GetPortfolioInfo(Guid userId)
        {
            var query = new GetUserPotrfolioQuery { UserId = userId };
            var result = await _mediator.Send(query);

            var jsonResult = JsonConvert.SerializeObject(result);
            return Ok(jsonResult);
        }
    }
}
