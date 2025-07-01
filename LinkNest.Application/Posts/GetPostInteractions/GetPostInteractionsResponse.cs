using LinkNest.Domain.Posts;

namespace LinkNest.Application.Posts.GetPostInteractions
{
    internal class InteractionInfo
    {
        public Guid UserProfileId { get; init; }
        public DateTime CreatedAt { get; init; }
        public InteractionTypes InteractionType { get; init; }
    }
    internal class GetPostInteractionsResponse
    {
        public Guid PostId { get; init; }
        public List<InteractionInfo> interactionInfos { get; init; }
    }
}
