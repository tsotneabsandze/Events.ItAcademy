using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using MEDIATOR.Common.Abstractions;
using MEDIATOR.Common.Models;
using MediatR;

namespace MEDIATOR.Account.Commands.SignInUser
{
    public class SignInUserCommand : IRequest<AuthResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public class SignInUserCommandHandler  : BaseRequestHandler<SignInUserCommand, AuthResult>
        {
            public SignInUserCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<AuthResult> Handle(SignInUserCommand request, CancellationToken cancellationToken)
            {
                var user = await AccountService.GetUserByEmailAsync(request.Email);
                
                if (user is null)
                    throw new UnauthorizedException();
                
                var result = await AccountService.SignInAsync(user,request.Password);
                
                
                var response = new AuthResult
                {
                    Email = request.Email
                };
                
                if (!result)
                    return response;
                
                response.Result = true;
                response.Email = request.Email;
                response.Id = user.Id;
                response.Token = await TokenService.CreateTokeAsync(user);

                return response;
            }
        }
        
    }
}