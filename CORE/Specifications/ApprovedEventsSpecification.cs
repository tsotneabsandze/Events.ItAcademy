using System;
using CORE.Entities;

namespace CORE.Specifications
{
    public class ApprovedEventsSpecification : BaseSpecification<Event>
    {
        public ApprovedEventsSpecification()
            : base(x => x.IsApproved && x.Ends > DateTime.Now)
        {
            AddOrderBy(x => x.Starts);
        }
    }
}