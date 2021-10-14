using System;
using System.Threading;
using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Models;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Account.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<AuthResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public class RegisterUserCommandHandler : BaseRequestHandler<RegisterUserCommand, AuthResult>
        {
            public RegisterUserCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<AuthResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    Name = request.Name,
                    LastName = request.LastName
                };
                
                var id = await AccountService.CreateAsync(user, request.Password, cancellationToken);
                
                
                var response = new AuthResult
                {
                    Email = request.Email
                };
                
                if (string.IsNullOrEmpty(id)) return response;
                
                response.Email = request.Email;
                response.Result = true;
                response.Id = user.Id;
                response.Token = await TokenService.CreateTokeAsync(user);
                
                return response;
            }
        }
        
    }
}