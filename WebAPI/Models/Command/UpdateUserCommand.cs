using System;

namespace WebAPI.Models.Command
{
    public class UpdateUserCommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }
    }
}
