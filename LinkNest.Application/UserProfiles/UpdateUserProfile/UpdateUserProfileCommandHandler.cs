using ApartmentBooking.Domain.Users;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Abstraction;
using LinkNest.Domain.UserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.UserProfiles.UpdateUserProfile
{
    internal class UpdateUserProfileCommandHandler : ICommandHandler<UpdateUserProfileCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.userProfileRepo.GetByIdAsync(request.Id);
            if (user == null)
                return Result.Failure(["No User Found"]);

            var isEmailTaken = await unitOfWork.userProfileRepo.IsEmailExist(request.Email, user.Email.email);
            if (isEmailTaken)
                return Result.Failure(["Email is already in use by another user."]);
            // Step 3: Update user profile fields
            user.Update(
                new FirstName (request.FirstName),
                new LastName( request.LastName),
                new UserProfileEmail( request.Email),
                request.DateOfBirth,
                new CurrentCity( request.CurrentCity)
            );

            // Step 4: Save changes
            await unitOfWork.SaveChangesAsync();

            return Result.Success();

        }
    }
}
