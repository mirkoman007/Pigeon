using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("IDCountry")]
        public int Idcountry { get; set; }
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty(nameof(City.Country))]
        public virtual ICollection<City> Cities { get; set; }
        [InverseProperty(nameof(Location.Country))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
