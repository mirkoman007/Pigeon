using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool Verification { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
    }
}
