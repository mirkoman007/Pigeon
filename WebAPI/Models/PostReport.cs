using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("PostReport")]
    public partial class PostReport
    {
        [Key]
        [Column("IDpostReport")]
        public int IdpostReport { get; set; }
        [StringLength(255)]
        public string Reason { get; set; }
        [StringLength(255)]
        public string Explanation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("PostID")]
        public int? PostId { get; set; }
        [Column("SenderID")]
        public int? SenderId { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("PostReports")]
        public virtual Post Post { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.PostReports))]
        public virtual User Sender { get; set; }
    }
}
