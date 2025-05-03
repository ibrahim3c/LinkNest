using LinkNest.Domain.Abstraction;

namespace LinkNest.Domain.Posts
{
    public class Post : Entity
    {
        public Post(Guid guid, Content content, DateTime createdAt, Url imageUrl) : base(guid)
        {
            Content = content;
            CreatedAt = createdAt;
            ImageUrl = imageUrl;
        }
        public Content Content {  get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public Url ImageUrl {  get; private set; }

    }
}
