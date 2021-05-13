using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public int Idcity { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
