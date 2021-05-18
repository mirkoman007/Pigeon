using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Command
{
    public class UpdateUserCommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }

        public bool Verification { get; set; }

        public int GenderId { get; set; }

        public int CityId { get; set; }
    }
}
