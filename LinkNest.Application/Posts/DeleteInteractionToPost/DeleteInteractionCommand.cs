using LinkNest.Application.Abstraction.Messaging;

namespace LinkNest.Application.Posts.DeleteInteractionToPost
{
    internal record DeleteInteractionCommand(Guid interactionId):ICommand;
}
