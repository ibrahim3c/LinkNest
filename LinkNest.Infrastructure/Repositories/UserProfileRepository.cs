using LinkNest.Domain.Posts;
using LinkNest.Domain.UserProfiles;
using LinkNest.Infrastructure.Data;
using MediatR;
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

        public async Task<bool> IsEmailExist(string email)
        {
            return await appDbContext.Set<UserProfile>().AnyAsync(u => u.Email.email == email);
        }

        public async Task<bool> IsEmailExist(string email,string except)
        {
            return await appDbContext.Set<UserProfile>().AnyAsync(u => u.Email.email == email && u.Email.email != except);
        }
        public void Update(UserProfile userProfile)
        {
             appDbContext.Set<UserProfile>().Update(userProfile);
        }
    }
}
