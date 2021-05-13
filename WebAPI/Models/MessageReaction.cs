using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("MessageReaction")]
    public partial class MessageReaction
    {
        [Key]
        [Column("IDMessageReaction")]
        public int IdmessageReaction { get; set; }
        [Column("MessageID")]
        public int? MessageId { get; set; }
        [Column("ReactionID")]
        public int? ReactionId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(MessageId))]
        [InverseProperty("MessageReactions")]
        public virtual Message Message { get; set; }
        [ForeignKey(nameof(ReactionId))]
        [InverseProperty("MessageReactions")]
        public virtual Reaction Reaction { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("MessageReactions")]
        public virtual User User { get; set; }
    }
}
