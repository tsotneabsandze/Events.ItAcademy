using System;
using CORE.Entities;

namespace CORE.Specifications
{
    public class ValidEventsCountSpecification : BaseSpecification<Event>
    {
        public ValidEventsCountSpecification()
            : base(x => x.Ends > DateTime.Now)
        {
        }
    }
}