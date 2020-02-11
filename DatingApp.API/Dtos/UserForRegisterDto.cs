using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        //data annotations
        [Required]
        //property
        public string Username { get; set; }

        [Required]
        //setting rules for password
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specifiy password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}