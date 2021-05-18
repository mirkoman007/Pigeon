using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public DateTime CreateDateTime { get; set; }

        [Required]
        public bool Verification { get; set; }

        [Required]
        public int UserTypeId { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
