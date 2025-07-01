using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Posts.GetPostComments
{
    internal sealed record GetPostInteractionsQuery(Guid postId):IQuery<GetPostInteractionsResponse>;
}
