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
            if (followerId == followeeId)
                throw new ArgumentException("User cannot follow themselves.");

            this.FollowerId = followerId;
            this.FolloweeId = followeeId;
        }

        public static Follow Create(Guid followerId, Guid followeeId)
        {
            return new Follow
            {
                FolloweeId = followerId,
                FollowerId = followerId,
                Guid= Guid.NewGuid()
            };
        }
        public UserProfile Follower {  get; private set; }
        public UserProfile Followee {  get; private set; }

    }
}
