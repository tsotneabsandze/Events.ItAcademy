using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Account.Commands.SignInUser
{
    public class SignInUserCommand : IRequest<AuthResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public class SignInUserCommandHandler  : IRequestHandler<SignInUserCommand, AuthResult>
        {
            private readonly ITokenService _tokenService;
            private readonly IAccountService _accountService;

            public SignInUserCommandHandler(ITokenService tokenService, IAccountService accountService)
            {
                _tokenService = tokenService;
                _accountService = accountService;
            }

            public async Task<AuthResult> Handle(SignInUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _accountService.GetUserByEmailAsync(request.Email);
                
                if (user is null)
                    throw new UnauthorizedException();

                var result = await _accountService.SignInAsync(user,request.Password);
                
                
                var response = new AuthResult();
                request.Email = request.Email;

                if (!result)
                    return response;

                response.Email = request.Email;
                response.Result = true;
                response.Token = await _tokenService.CreateTokeAsync(user);

                return response;
            }
        }
    }
}