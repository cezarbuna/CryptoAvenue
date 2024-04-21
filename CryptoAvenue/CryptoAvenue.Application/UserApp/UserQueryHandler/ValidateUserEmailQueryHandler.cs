using CryptoAvenue.Application.UserApp.UserQuery;
using CryptoAvenue.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.UserApp.UserQueryHandler
{
    public class ValidateUserEmailQueryHandler : IRequestHandler<ValidateUserEmailQuery, bool>
    {
        private readonly IUserRepository repository;

        public ValidateUserEmailQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(ValidateUserEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await repository.GetFirstEntityBy(x => x.Email == request.Email);
            if (user == null)
                return false;

            return true;
        }
    }
}
