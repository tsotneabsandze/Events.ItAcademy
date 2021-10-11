using System;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Account.Queries.GetUserDetails.GetUserDetailsById
{
    public class GetUserDetailsByIdQuery : IRequest<UserDetailsVm>
    {
        public string Id { get; set; }

        public class GetUserDetailsByIdQueryHandler : BaseRequestHandler<GetUserDetailsByIdQuery, UserDetailsVm>
        {
            public GetUserDetailsByIdQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<UserDetailsVm> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                var appUser = await AccountService.GetUserByIdAsync(request.Id);
                
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