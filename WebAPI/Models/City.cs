using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class City
    {
        public City()
        {
            Users = new HashSet<User>();
        }

        public int Idcity { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
