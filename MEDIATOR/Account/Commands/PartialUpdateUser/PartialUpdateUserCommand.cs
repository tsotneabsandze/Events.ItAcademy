using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Account.Commands.PartialUpdateUser
{
    public class PartialUpdateUserCommand : IRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public class PartialUpdateUserCommandHandler : BaseRequestHandler<PartialUpdateUserCommand,Unit>
        {
            public PartialUpdateUserCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<Unit> Handle(PartialUpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await AccountService.GetUserByIdAsync(request.Id);
                
                if (user is null)
                    throw new ResourceNotFoundException($"user with id {request.Id} was not found");
                
                await AccountService.UpdatePartialAsync(user, (request.Name, request.LastName, request.Email));
                
                return Unit.Value;
            }
        }
    }
}