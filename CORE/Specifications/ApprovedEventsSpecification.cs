using CORE.Entities;

namespace CORE.Specifications
{
    public class ApprovedEventsSpecification : BaseSpecification<Event>
    {
        public ApprovedEventsSpecification()
            : base(x => x.IsApproved)
        {
            AddOrderBy(x => x.Starts);
        }
    }
}