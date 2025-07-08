using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Posts;

namespace LinkNest.Application.Posts.AddCommentToPost
{
    internal record AddCommentCommand(Content Content, Guid PostId, Guid UserProfileId):ICommand<Guid>;
}
