using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Follows.FollowUserProfile
{
    internal record FollowCommand(Guid followeeId, Guid followingId):ICommand;
}
