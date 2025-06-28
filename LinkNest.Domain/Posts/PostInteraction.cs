using LinkNest.Domain.Abstraction;

namespace LinkNest.Domain.Posts
{
    public class PostInteraction : Entity
    {
        public PostInteraction(Guid guid, Guid postId,Guid userProfileId,DateTime createdAt,InteractionTypes interactionTypes) : base(guid)
        {
            PostId = postId;
            UserProfileId = userProfileId;
            CreatedAt = createdAt;
            InteractionType = interactionTypes;
        }
        private PostInteraction() { }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public InteractionTypes InteractionType { get; private set; }
        // Navigation properties
        public Post Post { get; private set; } = null!; // Ensures Post is not null after initialization


    }
}
