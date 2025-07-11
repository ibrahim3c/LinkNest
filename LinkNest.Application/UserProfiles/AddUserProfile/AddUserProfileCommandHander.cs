
using ApartmentBooking.Domain.Users;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Abstraction;
using LinkNest.Domain.UserProfiles;
using MediatR;

namespace LinkNest.Application.UserProfiles.AddUserProfile
{
    internal class AddUserProfileCommandHander : ICommandHandler<AddUserProfileCommand,Guid>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddUserProfileCommandHander(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        async Task<Result<Guid>> IRequestHandler<AddUserProfileCommand, Result<Guid>>.Handle(AddUserProfileCommand request, CancellationToken cancellationToken)
        {

            if (await unitOfWork.userProfileRepo.IsEmailExist(request.Email))
                return Result<Guid>.Failure(["The Email Already Exists"]);

            var user = UserProfile.Create(new FirstName (request.FirstName),
                new LastName( request.LastName),
                new UserProfileEmail( request.Email),
                request.DateOfBirth,
                new CurrentCity( request.CurrentCity));

            await unitOfWork.userProfileRepo.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            return  Result<Guid>.Success(user.Guid);
        }
    }
}
