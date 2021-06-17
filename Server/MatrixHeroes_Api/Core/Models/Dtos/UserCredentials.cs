using System.ComponentModel.DataAnnotations;

namespace MatrixHeroes_Api.Core.Models.Dtos
{
    public class UserCredentials
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(2)]
        public string Role { get; set; } = "Trainer";
    }
}