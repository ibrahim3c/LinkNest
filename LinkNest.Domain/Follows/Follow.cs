using LinkNest.Domain.Abstraction;

namespace LinkNest.Domain.Follows
{
    public class Follow(Guid guid, Guid followeeId) : Entity(guid)
    {
        public Guid FolloweeId { get; private set; } = followeeId;
    }
}
