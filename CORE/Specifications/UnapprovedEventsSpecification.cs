using System;
using CORE.Entities;

namespace CORE.Specifications
{
    public class UnapprovedEventsSpecification : BaseSpecification<Event>
    {
        public UnapprovedEventsSpecification()
            : base(x => !x.IsApproved && x.Ends > DateTime.Now)
        {
            AddOrderBy(x => x.Starts);
        }
    }
}