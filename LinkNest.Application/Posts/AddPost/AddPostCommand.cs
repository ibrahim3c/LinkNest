using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Posts;

namespace LinkNest.Application.Posts.AddPost
{
    internal record AddPostCommand(Content Content,  Url ImageUrl , Guid UserProfileId) :ICommand<Guid>;
}
