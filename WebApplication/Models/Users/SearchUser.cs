using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Users
{
    public class SearchUser
    {
        public int IdUser { get; set; }
        public string FirstLastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
    }
}
