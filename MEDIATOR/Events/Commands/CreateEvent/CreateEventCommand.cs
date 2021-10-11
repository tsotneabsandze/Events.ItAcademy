using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using Mapster;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public byte[] Photo { get; set; }

        
        public class CreateEventCommandHandler : BaseRequestHandler<CreateEventCommand,Unit>
        {
            public CreateEventCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
            {
                var entity = request.Adapt<Event>();
                entity.UserId = UserService.GetUser().Claims.First(x=>x.Type=="id").Value;
                await EventRepo.InsertAsync(entity, cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}