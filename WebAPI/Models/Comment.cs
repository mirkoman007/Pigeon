using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        public Comment()
        {
            CommentReactions = new HashSet<CommentReaction>();
            CommentReports = new HashSet<CommentReport>();
            InverseCommentNavigation = new HashSet<Comment>();
        }

        [Key]
        [Column("IDComment")]
        public int Idcomment { get; set; }
        [StringLength(255)]
        public string Text { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("PostID")]
        public int? PostId { get; set; }
        [Column("CommentID")]
        public int? CommentId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(CommentId))]
        [InverseProperty(nameof(Comment.InverseCommentNavigation))]
        public virtual Comment CommentNavigation { get; set; }
        [ForeignKey(nameof(PostId))]
        [InverseProperty("Comments")]
        public virtual Post Post { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Comments")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(CommentReaction.Comment))]
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        [InverseProperty(nameof(CommentReport.Comment))]
        public virtual ICollection<CommentReport> CommentReports { get; set; }
        [InverseProperty(nameof(Comment.CommentNavigation))]
        public virtual ICollection<Comment> InverseCommentNavigation { get; set; }
    }
}
