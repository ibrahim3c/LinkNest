using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Posts.GetPostInteractions
{
    internal sealed record GetPostInteractionsQuery(Guid postId):IQuery<GetPostInteractionsResponse>;
}
