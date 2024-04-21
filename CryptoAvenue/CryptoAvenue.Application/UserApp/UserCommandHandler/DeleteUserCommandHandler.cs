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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
    {
        private readonly IUserRepository repository;

        public DeleteUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetEntityByID(request.UserId);
            if (user == null)
                return null;

            repository.Delete(user);
            await repository.SaveChanges();

            return user;
        }
    }
}
