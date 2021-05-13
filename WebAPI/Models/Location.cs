using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Location")]
    public partial class Location
    {
        public Location()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("IDLocation")]
        public int Idlocation { get; set; }
        [Column("CityID")]
        public int? CityId { get; set; }
        [Column("CountryID")]
        public int? CountryId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Locations")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(CountryId))]
        [InverseProperty("Locations")]
        public virtual Country Country { get; set; }
        [InverseProperty(nameof(User.Location))]
        public virtual ICollection<User> Users { get; set; }
    }
}
