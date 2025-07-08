using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Follows.UnFollowUserProfile
{
    internal record UnFollowCommand(Guid followeeId, Guid followerId) : ICommand;

}
