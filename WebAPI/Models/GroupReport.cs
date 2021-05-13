using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("GroupReport")]
    public partial class GroupReport
    {
        [Key]
        [Column("IDGroupReport")]
        public int IdgroupReport { get; set; }
        [StringLength(255)]
        public string Reason { get; set; }
        [StringLength(255)]
        public string Explanation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("GroupID")]
        public int? GroupId { get; set; }
        [Column("UserSenderID")]
        public int? UserSenderId { get; set; }
        public bool? Approved { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupReports")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(UserSenderId))]
        [InverseProperty(nameof(User.GroupReports))]
        public virtual User UserSender { get; set; }
    }
}
