using LinkNest.Domain.Posts;
using LinkNest.Domain.UserProfiles;
using LinkNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkNest.Infrastructure.Repositories
{
    internal class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext appDbContext;
        public UserProfileRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task AddAsync(UserProfile userProfile)
        {
            await appDbContext.Set<UserProfile>().AddAsync(userProfile);

        }

        public async Task<UserProfile> GetByIdAsync(Guid userProfileId)
        {
           return await appDbContext.Set<UserProfile>().FirstOrDefaultAsync(u=>u.Guid==userProfileId);
        }

        public void Update(UserProfile userProfile)
        {
             appDbContext.Set<UserProfile>().Update(userProfile);
        }
    }
}
