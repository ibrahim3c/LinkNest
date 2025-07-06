using ApartmentBooking.Domain.Users;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.UserProfiles;

namespace LinkNest.Application.UserProfiles.UpdateUserProfile
{
        internal record UpdateUserProfileCommand(
            Guid Id,
            FirstName FirstName,
            LastName LastName,
            UserProfileEmail Email,
            DateTime DateOfBirth,
            CurrentCity CurrentCity
         ) : ICommand;
}
