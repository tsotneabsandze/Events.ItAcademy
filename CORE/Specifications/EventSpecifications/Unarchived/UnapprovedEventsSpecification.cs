using CORE.Entities;

namespace CORE.Specifications.EventSpecifications.Unarchived
{
    public class UnapprovedEventsSpecification : BaseSpecification<Event>
    {
        public UnapprovedEventsSpecification()
            : base(x => !x.IsApproved && !x.IsArchived)
        {
            AddOrderBy(x => x.Starts);
        }
    }
}