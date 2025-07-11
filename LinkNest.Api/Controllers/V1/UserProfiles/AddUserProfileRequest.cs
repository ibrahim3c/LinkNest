namespace LinkNest.Api.Controllers.V1.UserProfiles
{
    public record AddUserProfileRequest(
        string FirstName,
        string LastName,
        string Email,
        DateTime DateOfBirth,
        string CurrentCity
     );
}
