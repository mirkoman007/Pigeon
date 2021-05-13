using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Reaction")]
    public partial class Reaction
    {
        public Reaction()
        {
            CommentReactions = new HashSet<CommentReaction>();
            MessageReactions = new HashSet<MessageReaction>();
            PostReactions = new HashSet<PostReaction>();
        }

        [Key]
        [Column("IDReaction")]
        public int Idreaction { get; set; }
        [StringLength(255)]
        public string Value { get; set; }

        [InverseProperty(nameof(CommentReaction.Reaction))]
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        [InverseProperty(nameof(MessageReaction.Reaction))]
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        [InverseProperty(nameof(PostReaction.Reaction))]
        public virtual ICollection<PostReaction> PostReactions { get; set; }
    }
}
