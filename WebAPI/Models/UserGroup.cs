using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("UserGroup")]
    public partial class UserGroup
    {
        [Key]
        [Column("IDUserGroup")]
        public int IduserGroup { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("GroupID")]
        public int? GroupId { get; set; }
        public int? UserType { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("UserGroups")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserGroups")]
        public virtual User User { get; set; }
    }
}
