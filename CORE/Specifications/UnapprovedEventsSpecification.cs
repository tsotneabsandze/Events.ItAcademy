using CORE.Entities;

namespace CORE.Specifications
{
    public class UnapprovedEventsSpecification : BaseSpecification<Event>
    {
        public UnapprovedEventsSpecification() : base(x => !x.IsApproved)
        {
            AddOrderBy(x=>x.Starts);
        }
    }
}