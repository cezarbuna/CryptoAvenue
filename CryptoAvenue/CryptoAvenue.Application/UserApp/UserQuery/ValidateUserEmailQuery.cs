using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.UserApp.UserQuery
{
    public class ValidateUserEmailQuery : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
