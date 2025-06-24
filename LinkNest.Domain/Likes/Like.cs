using LinkNest.Domain.Abstraction;

namespace LinkNest.Domain.Likes
{
    public class Like : Entity
    {
        public Like(Guid guid, Guid postId, DateTime createdAt) : base(guid)
        {
            PostId = postId;
            CreatedAt = createdAt;
        }
        private Like() { }  
        public Guid PostId { get; private set; }
        public DateTime CreatedAt { get; private set; }= DateTime.Now;
    }
}
