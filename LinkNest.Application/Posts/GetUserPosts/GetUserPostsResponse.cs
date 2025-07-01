using LinkNest.Application.Posts.GetPost;

namespace LinkNest.Application.Posts.GetUserPosts
{
    internal sealed class GetUserPostsResponse
    {
        public Guid userProfileId {  get; init; }
        public List<GetPostResponse> posts { get; init; }

    }
}
