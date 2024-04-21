using AutoMapper;
using CryptoAvenue.Application.UserApp.UserCommands;
using CryptoAvenue.Application.UserApp.UserQuery;
using CryptoAvenue.Dal.Dtos.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public UsersController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserPostDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateUserCommand
            {
                Email = userDto.Email,
                Username = userDto.Username,
                Age = userDto.Age,
                Password = userDto.Password
            };

            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<UserGetDto>(result);

            return CreatedAtAction(nameof(GetUserById), new { userId = mappedResult.Id }, mappedResult);
        }
        [HttpGet]
        [Route("get-user-by-id/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var query = new GetUserByIdQuery { UserId = userId };
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<UserGetDto>(result);
            return Ok(mappedResult);
        }
        [HttpGet]
        [Route("validate-user-email/{email}")]
        public async Task<IActionResult> ValidateUserEmail(string email)
        {
            var query = new ValidateUserEmailQuery { Email = email };
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var command = new DeleteUserCommand { UserId = userId };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}
