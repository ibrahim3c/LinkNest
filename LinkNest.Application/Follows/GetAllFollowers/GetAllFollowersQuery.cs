using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Follows.GetAllFollowers
{
    internal sealed record GetAllFollowersQuery(Guid UserProfileId):IQuery<GetAllFollowersResponse>;
}
