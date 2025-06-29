using LinkNest.Domain.Abstraction;
using LinkNest.Domain.UserProfiles;

namespace LinkNest.Domain.Follows
{
    public class Follow : Entity
    {
        public Guid FollowerId { get; private set; } // who follows
        public Guid FolloweeId { get; private set; } // who is being followed

        private Follow()
        {
            
        }
        public Follow(Guid id, Guid followerId, Guid followeeId):base(id) 
        {
            this.FollowerId = followerId;
            this.FolloweeId = followeeId;
        }
        public UserProfile Follower {  get; private set; }
        public UserProfile Followee {  get; private set; }
    }
}
