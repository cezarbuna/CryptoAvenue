using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.UserApp.UserCommands
{
    public class LoginUser : IRequest<LoginResponse>
    {
        public LoginModel Model { get; set; }
    }
}
