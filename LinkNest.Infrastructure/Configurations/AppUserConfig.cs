using LinkNest.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkNest.Infrastructure.Configurations
{
    internal sealed class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(user=>user.UserProfile)
                .WithOne()
                .HasForeignKey<AppUser>(up => up.UserProfileId);
        }
    }
}
