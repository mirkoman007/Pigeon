using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("City")]
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("IDCity")]
        public int Idcity { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [Column("CountryID")]
        public int? CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty("Cities")]
        public virtual Country Country { get; set; }
        [InverseProperty(nameof(Location.City))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
