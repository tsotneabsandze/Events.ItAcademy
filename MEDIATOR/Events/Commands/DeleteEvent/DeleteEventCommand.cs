using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Exceptions;
using CORE.Interfaces;
using MediatR;

namespace MEDIATOR.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
        {
            private readonly IGenericRepository<Event> _repository;

            public DeleteEventCommandHandler(IGenericRepository<Event> repository)
            {
                _repository = repository;
            }

            public  async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

                if (entity is null)
                    throw new ResourceNotFoundException($"event with id {request.Id} was not found");

                await _repository.DeleteAsync(entity, cancellationToken: cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}