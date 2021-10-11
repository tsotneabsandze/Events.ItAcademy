using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Account.Queries.GetUsersList
{
    public class GetUsersListQuery: IRequest<UserListVm>
    {
        public class GetUsersListQueryHandler : BaseRequestHandler<GetUsersListQuery, UserListVm>
        {
            public GetUsersListQueryHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<UserListVm> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
            {
                var users = await AccountService.GetAllAsync(cancellationToken);
                
                var vm = new UserListVm
                {
                    Users = users.Adapt<ICollection<UserLookupDto>>()
                };
                
                return vm;
            }
        }
    }
}