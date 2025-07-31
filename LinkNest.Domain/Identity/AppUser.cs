using LinkNest.Domain.UserProfiles;
using Microsoft.AspNetCore.Identity;

namespace LinkNest.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
