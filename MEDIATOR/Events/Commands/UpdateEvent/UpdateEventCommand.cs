using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CORE.Exceptions;
using MEDIATOR.Common.Abstractions;
using MediatR;

namespace MEDIATOR.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public byte[] Photo { get; set; }
        public string UserId { get; set; }


        public class UpdateEventCommandHandler : BaseRequestHandler<UpdateEventCommand, Unit>
        {
            public UpdateEventCommandHandler(IServiceProvider service) : base(service)
            {
            }

            public override async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
            {
                var entity = await EventRepo.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

                if (entity is null)
                    throw new ResourceNotFoundException($"resource with id {request.Id} was not found");

                if (request.UserId != UserService.GetUser().Claims.First(x => x.Type == "id").Value)
                    throw new IdentifierMismatchException("event does not belong to specified user");

                if (!(DateTime.Now < entity.CanBeEditedTill && entity.IsApproved))
                    throw new ResourceCanNotBeEditedException();


                entity.Description = request.Description;
                entity.Starts = request.Starts;
                entity.Ends = request.Ends;
                if (request.Photo != default)
                    entity.Photo = request.Photo;


                await EventRepo.UpdateAsync(entity, cancellationToken);

                return Unit.Value;
            }
        }
    }
}