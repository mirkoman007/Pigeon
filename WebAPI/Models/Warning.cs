using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Warning")]
    public partial class Warning
    {
        [Key]
        [Column("IDWarning")]
        public int Idwarning { get; set; }
        [StringLength(255)]
        public string Reason { get; set; }
        [StringLength(255)]
        public string Explanation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("AdminID")]
        public int? AdminId { get; set; }

        [ForeignKey(nameof(AdminId))]
        [InverseProperty("WarningAdmins")]
        public virtual User Admin { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("WarningUsers")]
        public virtual User User { get; set; }
    }
}
