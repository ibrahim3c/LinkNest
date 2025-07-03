using LinkNest.Domain.Posts;

namespace LinkNest.Domain.UserProfiles
{
    public interface IUserProfileRepository
    {
        Task AddAsync(UserProfile userProfile);
        void Update(UserProfile userProfile);
        Task<UserProfile> GetByIdAsync(Guid userProfileId);
    }
}
