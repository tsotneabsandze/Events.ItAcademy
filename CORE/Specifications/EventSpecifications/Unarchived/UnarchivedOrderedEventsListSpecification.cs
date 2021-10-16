using CORE.Entities;

namespace CORE.Specifications.EventSpecifications.Unarchived
{
    public class UnarchivedOrderedEventsListSpecification : BaseSpecification<Event>
    {
        public UnarchivedOrderedEventsListSpecification(SpecParams specParams)
            : base(x => !x.IsArchived)
        {
            AddOrderBy(x => x.Starts);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}