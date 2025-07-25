using System.ComponentModel.DataAnnotations;

namespace Nzwalks_api.Models.DTO.Requestes
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6,
           ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
