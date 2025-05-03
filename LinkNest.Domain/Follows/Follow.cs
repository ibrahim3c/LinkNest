using LinkNest.Domain.Abstraction;

namespace LinkNest.Domain.Follows
{
    public class Follow : Entity
    {
        public Follow(Guid guid, Guid followeeId) : base(guid)
        {
            FolloweeId = followeeId;
        }

        public Guid FolloweeId {  get; private set; }
    }
}
