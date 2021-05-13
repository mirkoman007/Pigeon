using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("CommentReport")]
    public partial class CommentReport
    {
        [Key]
        [Column("IDCommentReport")]
        public int IdcommentReport { get; set; }
        [StringLength(255)]
        public string Reason { get; set; }
        [StringLength(255)]
        public string Explanation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("CommentID")]
        public int? CommentId { get; set; }
        [Column("SenderID")]
        public int? SenderId { get; set; }

        [ForeignKey(nameof(CommentId))]
        [InverseProperty("CommentReports")]
        public virtual Comment Comment { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.CommentReports))]
        public virtual User Sender { get; set; }
    }
}
