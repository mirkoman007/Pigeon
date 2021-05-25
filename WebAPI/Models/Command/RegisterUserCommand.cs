using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Command
{
    public class RegisterUserCommand
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string City { get; set; }

    }
}
