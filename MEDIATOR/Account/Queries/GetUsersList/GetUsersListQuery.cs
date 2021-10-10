using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Mapster;
using MediatR;

namespace MEDIATOR.Account.Queries.GetUsersList
{
    public class GetUsersListQuery: IRequest<UserListVm>
    {
        public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery,UserListVm>
        {
            private readonly IAccountService _accountService;

            public GetUsersListQueryHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<UserListVm> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
            {
                var users = await _accountService.GetAllAsync(cancellationToken);

                var vm = new UserListVm
                {
                    Users = users.Adapt<ICollection<UserLookupDto>>()
                };

                return vm;
            }
        }
    }
}