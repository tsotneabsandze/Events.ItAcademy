using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsByEmail
{
    public class GetUserDetailsByEmailQuery : IRequest<UserDetailsVm>
    {
        public string Email { get; set; }

        public class GetUserDetailsByEmailQueryHandler : BaseRequestHandler<GetUserDetailsByEmailQuery, UserDetailsVm>
        {
            public GetUserDetailsByEmailQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<UserDetailsVm> Handle(GetUserDetailsByEmailQuery request, CancellationToken cancellationToken)
            {
                var appUser = await AccountService.GetUserByEmailAsync(request.Email);
                
                if (appUser is null)
                    throw new ResourceNotFoundException("User not found");
                
                
                var roles = await AccountService.GetRolesForUserAsync(appUser);
                
                var vm = appUser.Adapt<UserDetailsVm>();
                vm.Roles = roles;
                
                return vm;
            }
        }

        
    }
}