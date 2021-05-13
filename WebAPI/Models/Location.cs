using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Location
    {
        public Location()
        {
            Users = new HashSet<User>();
        }

        public int Idlocation { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
