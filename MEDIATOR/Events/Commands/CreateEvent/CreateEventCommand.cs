using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using INFRASTRUCTURE.Identity.Services.Abstractions;
using Mapster;
using MEDIATOR.Account.Queries.GetUserDetails;
using MediatR;

namespace MEDIATOR.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CanBeEditedTill { get; set; }
        public byte[] Photo { get; set; }

        public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
        {
            private readonly IGenericRepository<Event> _repository;
            private readonly IUserService _userService;

            public CreateEventCommandHandler(IGenericRepository<Event> repository, IUserService userService)
            {
                _repository = repository;
                _userService = userService;
            }

            public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
            {
                var entity = request.Adapt<Event>();
                entity.UserId = _userService.GetUser().Claims.First(x=>x.Type=="id").Value;
                await _repository.InsertAsync(entity, cancellationToken);

                return Unit.Value;
            }
        }
    }
}