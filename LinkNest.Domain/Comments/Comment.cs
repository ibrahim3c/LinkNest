using LinkNest.Domain.Abstraction;
using LinkNest.Domain.Posts;

namespace LinkNest.Domain.Comments
{
    public class Comment : Entity
    {
        public Comment(Guid guid, Content content, DateTime createdAt) : base(guid)
        {
            Content = content;
            CreatedAt = createdAt;
        }
        public Content Content { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public Guid PostId { get; private set; }
    }
}
