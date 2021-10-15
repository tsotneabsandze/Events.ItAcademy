using CORE.Entities;

namespace CORE.Specifications
{
    public class OrderedEventsListSpecification : BaseSpecification<Event>
    {
        public OrderedEventsListSpecification(SpecParams specParams)
        {
            AddOrderBy(x => x.Starts);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}