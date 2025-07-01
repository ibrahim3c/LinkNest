namespace LinkNest.Application.Posts.GetPostComments
{
    internal class CommentInfo
    {
        public string Content { get; init; }
        public DateTime CreatedAt { get; init; }
        public Guid UserProfileId { get; init; }
    }
    internal class GetPostInteractionsResponse
    {
        public Guid PostId { get; init; }
        public List<CommentInfo> PostComments { get; init; }


    }
}
