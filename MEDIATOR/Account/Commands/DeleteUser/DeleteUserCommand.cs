using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using MediatR;

namespace MEDIATOR.Account.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string Email { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IAccountService _accountService;

            public DeleteUserCommandHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _accountService.GetUserByEmailAsync(request.Email);

                if (user is null)
                    throw new ResourceNotFoundException("user not found");

                await _accountService.DeleteUserAsync(user);

                return Unit.Value;
            }
        }
    }
}