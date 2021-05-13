using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("PostReaction")]
    public partial class PostReaction
    {
        [Key]
        [Column("IDPostReaction")]
        public int IdpostReaction { get; set; }
        [Column("PostID")]
        public int? PostId { get; set; }
        [Column("ReactionID")]
        public int? ReactionId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("PostReactions")]
        public virtual Post Post { get; set; }
        [ForeignKey(nameof(ReactionId))]
        [InverseProperty("PostReactions")]
        public virtual Reaction Reaction { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("PostReactions")]
        public virtual User User { get; set; }
    }
}
