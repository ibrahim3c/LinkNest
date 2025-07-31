using System.ComponentModel.DataAnnotations;

namespace LinkNest.Application.DTOs.Auth
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
