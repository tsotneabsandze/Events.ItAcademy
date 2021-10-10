using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Mapster;
using MediatR;

namespace MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsById
{
    public class GetUserDetailsByIdQuery : IRequest<UserDetailsVm>
    {
        public string Id { get; set; }

        public class GetUserDetailsByIdQueryHandler : IRequestHandler<GetUserDetailsByIdQuery,UserDetailsVm>
        {
            private readonly IAccountService _accountService;

            public GetUserDetailsByIdQueryHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<UserDetailsVm> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                var appUser = await _accountService.GetUserByIdAsync(request.Id);
                
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