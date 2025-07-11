using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.UserProfiles.AddUserProfile
{
    internal class AddUserCommandValidator : AbstractValidator<AddUserProfileCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First name is required");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("Last name is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid email format");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past");

            RuleFor(x => x.CurrentCity)
                .NotNull().WithMessage("Current city is required");
        }
    }
}