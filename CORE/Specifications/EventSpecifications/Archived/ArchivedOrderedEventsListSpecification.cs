using CORE.Entities;

namespace CORE.Specifications.EventSpecifications.Archived
{
    public class ArchivedOrderedEventsListSpecification : BaseSpecification<Event>
    {
        public ArchivedOrderedEventsListSpecification()
            : base(x => x.IsArchived)
        {
            AddOrderBy(x => x.Starts);
        }
    }
}