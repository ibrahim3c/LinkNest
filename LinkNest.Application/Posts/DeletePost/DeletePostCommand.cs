
using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Posts.DeletePost
{
    internal record DeletePostCommand(Guid postId):ICommand;
}
