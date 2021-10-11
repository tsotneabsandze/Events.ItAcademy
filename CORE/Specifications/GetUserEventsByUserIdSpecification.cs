using CORE.Entities;

namespace CORE.Specifications
{
    public class GetUserEventsByUserIdSpecification : BaseSpecification<Event>
    {
        public GetUserEventsByUserIdSpecification(string userId)
            : base(x => x.UserId == userId)
        {
        }
    }
}