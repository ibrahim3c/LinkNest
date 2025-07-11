using ApartmentBooking.Domain.Users;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.UserProfiles;

namespace LinkNest.Application.UserProfiles.AddUserProfile
{
    public record AddUserProfileCommand(
        string FirstName,
        string LastName,
        string Email,
        DateTime DateOfBirth,
        string CurrentCity
     ): ICommand<Guid>; 
}
