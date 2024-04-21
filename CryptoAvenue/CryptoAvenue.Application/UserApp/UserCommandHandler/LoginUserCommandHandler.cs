using CryptoAvenue.Application.Services;
using CryptoAvenue.Application.UserApp.UserCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.UserApp.UserCommandHandler
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUser, LoginResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly JwtTokenService tokenService;

        public LoginUserCommandHandler(IUserRepository userRepository, JwtTokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetEntityBy(x => x.Email == request.Model.Email && x.Password == request.Model.Password);
            if(user != null)
            {
                return new LoginResponse
                {
                    Email = user.Email,
                    UserId = user.Id,
                    Token = tokenService.GenerateToken(user.Email, user.Password)
                };
            }
            return null;
        }
    }
}
