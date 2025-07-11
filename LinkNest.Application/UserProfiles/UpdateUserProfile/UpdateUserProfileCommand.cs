using ApartmentBooking.Domain.Users;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.UserProfiles;
using System.Collections.Specialized;

namespace LinkNest.Application.UserProfiles.UpdateUserProfile
{
        public record UpdateUserProfileCommand(
            Guid Id,
            string FirstName,
            string LastName,
            string Email,
            DateTime DateOfBirth,
            string CurrentCity
         ) : ICommand;
}
