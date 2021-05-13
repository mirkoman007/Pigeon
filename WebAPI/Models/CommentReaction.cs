using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("CommentReaction")]
    public partial class CommentReaction
    {
        [Key]
        [Column("IDCommentReaction")]
        public int IdcommentReaction { get; set; }
        [Column("CommentID")]
        public int? CommentId { get; set; }
        [Column("ReactionID")]
        public int? ReactionId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(CommentId))]
        [InverseProperty("CommentReactions")]
        public virtual Comment Comment { get; set; }
        [ForeignKey(nameof(ReactionId))]
        [InverseProperty("CommentReactions")]
        public virtual Reaction Reaction { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("CommentReactions")]
        public virtual User User { get; set; }
    }
}
