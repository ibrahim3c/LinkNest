using ApartmentBooking.Domain.Users;
using LinkNest.Domain.Abstraction;
using LinkNest.Domain.Follows;
using LinkNest.Domain.Posts;

namespace LinkNest.Domain.UserProfiles
{
    public class UserProfile : Entity
    {
        public UserProfile(Guid guid, FirstName firstName, LastName lastName, UserProfileEmail email, DateTime dateOfBirth, DateTime createdOn, CurrentCity currentCity) : base(guid)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            CreatedOn = createdOn;
            CurrentCity = currentCity;
        }
        // for EF Core
        private UserProfile() : base() { }

        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public UserProfileEmail Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public CurrentCity CurrentCity { get; private set; }

        // nav properties
        public ICollection<Post> Posts { get; private set; } = new List<Post>();

        public ICollection<Follow> Following { get; set; } = new List<Follow>();   // Users this user is following
        public ICollection<Follow> Followers { get; set; } = new List<Follow>(); // Users who follow this user


        // factory method
        public static UserProfile Create(FirstName firstName, LastName lastName, UserProfileEmail email,DateTime dateOfBirth,CurrentCity currentCity)
        {
            //To Do - validate the parameters & error handling & raise domain events if needed

            var user = new UserProfile(Guid.NewGuid(), firstName, lastName, email,dateOfBirth,DateTime.UtcNow,currentCity);
            //user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
            return user;
        }
        public void Update(FirstName firstName, LastName lastName, UserProfileEmail email, DateTime dateOfBirth, CurrentCity currentCity)
        {
            //To Do - validate the parameters & error handling & raise domain events if needed
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            CurrentCity = currentCity;
        }

    }
}
