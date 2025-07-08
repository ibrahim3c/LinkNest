using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Posts;

namespace LinkNest.Application.Posts.UpdatePostContent
{
    internal record UpdatePostContentCommand(Guid postId,Content Content):ICommand;
}
