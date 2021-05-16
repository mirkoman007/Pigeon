using System;
using System.Collections.Generic;

namespace WebAPI.Models.DTO
{
    public class UserDto
    {
        public int IdUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int GenderID { get; set; }

        public int CityID { get; set; }

        public int UserTypeID { get; set; }

    }

}
