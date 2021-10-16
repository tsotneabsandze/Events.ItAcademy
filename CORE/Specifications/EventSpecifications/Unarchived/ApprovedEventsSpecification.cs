using CORE.Entities;

namespace CORE.Specifications.EventSpecifications.Unarchived
{
    public class ApprovedEventsSpecification : BaseSpecification<Event>
    {
        public ApprovedEventsSpecification()
            : base(x => x.IsApproved && !x.IsArchived)
        {
            AddOrderBy(x => x.Starts);
        }
    }
}