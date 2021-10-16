using CORE.Entities;

namespace CORE.Specifications.EventSpecifications.Unarchived
{
    public class GetUserEventsByUserIdSpecification : BaseSpecification<Event>
    {
        public GetUserEventsByUserIdSpecification(string userId)
            : base(x => x.UserId == userId && !x.IsArchived)
        {
        }
    }
}