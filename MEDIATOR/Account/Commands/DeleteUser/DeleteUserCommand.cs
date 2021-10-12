using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Account.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string Email { get; set; }

        public class DeleteUserCommandHandler : BaseRequestHandler<DeleteUserCommand,Unit>
        {
            public DeleteUserCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await AccountService.GetUserByEmailAsync(request.Email);

                if (await AccountService.IsInRole(user, "Admin"))
                    throw new InvalidCrudOperationException("Admin can not be deleted");
                
                if (user is null)
                    throw new ResourceNotFoundException("user not found");
                
                await AccountService.DeleteUserAsync(user);
                
                return Unit.Value;
            }
        }
    }
}