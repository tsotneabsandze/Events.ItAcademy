using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using MediatR;

namespace MEDIATOR.Account.Commands.PartialUpdateUser
{
    public class PartialUpdateUserCommand : IRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public class PartialUpdateUserCommandHandler : IRequestHandler<PartialUpdateUserCommand>
        {
            private readonly IAccountService _accountService;

            public PartialUpdateUserCommandHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<Unit> Handle(PartialUpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _accountService.GetUserByIdAsync(request.Id);

                if (user is null)
                    throw new ResourceNotFoundException($"user with id {request.Id} was not found");

                await _accountService.UpdatePartialAsync(user, (request.Name, request.LastName, request.Email));

                return Unit.Value;
            }
        }
    }
}