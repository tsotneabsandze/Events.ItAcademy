using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Mapster;
using MediatR;

namespace MEDIATOR.Account.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailsVm>
    {
        public string Email { get; set; }

        public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
        {
            private readonly IAccountService _accountService;

            public GetUserDetailsQueryHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
            {
                var appUser = await _accountService.GetUserByEmailAsync(request.Email);
                
                if (appUser is null)
                    throw new ResourceNotFoundException("User not found");
                
                
                var roles = await _accountService.GetRolesForUserAsync(appUser);
                
                var vm = appUser.Adapt<UserDetailsVm>();
                vm.Roles = roles;

                return vm;
            }
        }
    }
}