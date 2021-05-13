using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Locations = new HashSet<Location>();
        }

        public int Idcountry { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
