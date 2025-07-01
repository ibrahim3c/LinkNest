using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Posts.GetUserPosts
{
    internal sealed record GetUserPostsQuery(Guid UserProfileId):IQuery<GetUserPostsResponse>;
}
