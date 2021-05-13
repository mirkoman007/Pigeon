using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("UserReport")]
    public partial class UserReport
    {
        [Key]
        [Column("IDUserReport")]
        public int IduserReport { get; set; }
        [StringLength(255)]
        public string Reason { get; set; }
        [StringLength(255)]
        public string Explanation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("UserSenderID")]
        public int? UserSenderId { get; set; }
        public bool? Approved { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserReportUsers")]
        public virtual User User { get; set; }
        [ForeignKey(nameof(UserSenderId))]
        [InverseProperty("UserReportUserSenders")]
        public virtual User UserSender { get; set; }
    }
}
