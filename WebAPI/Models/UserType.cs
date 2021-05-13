using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("UserType")]
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("IDUserType")]
        public int IduserType { get; set; }
        [StringLength(255)]
        public string Value { get; set; }

        [InverseProperty(nameof(User.UserType))]
        public virtual ICollection<User> Users { get; set; }
    }
}
