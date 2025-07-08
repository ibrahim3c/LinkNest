using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Posts.DeleteCommentToPost
{
    internal record DeleteCommentCommand(Guid CommentId):ICommand;
}
