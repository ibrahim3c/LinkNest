namespace LinkNest.Application.DTOs.Auth
{
    public class RegisterDTO
    {
        public string Fname { get; set; } = default!;
        public string Sname { get; set; } = default!;
        public string Tname { get; set; } = default!;
        public string Lname { get; set; } = default!;
        public string CurrentCity {  get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime BirthDate { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
