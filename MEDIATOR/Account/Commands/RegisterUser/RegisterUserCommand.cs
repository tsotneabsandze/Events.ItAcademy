using System.Threading;
using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Models;
using INFRASTRUCTURE.Identity.Services.Abstractions;
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

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResult>
        {
            private readonly IAccountService _accountService;
            private readonly ITokenService _tokenService;

            public RegisterUserCommandHandler(IAccountService accountService, ITokenService tokenService)
            {
                _accountService = accountService;
                _tokenService = tokenService;
            }

            public async Task<AuthResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    Name = request.Name,
                    LastName = request.LastName
                };

                var id = await _accountService.CreateAsync(user, request.Password, cancellationToken);


                var response = new AuthResult();
                request.Email = request.Email;

                if (string.IsNullOrEmpty(id)) return response;

                response.Email = request.Email;
                response.Result = true;
                response.Token = await _tokenService.CreateTokeAsync(user);

                return response;
            }
        }
    }
}