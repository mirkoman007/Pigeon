using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class CommentReaction
    {
        public int IdcommentReaction { get; set; }
        public int? CommentId { get; set; }
        public int? ReactionId { get; set; }
        public int? UserId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Reaction Reaction { get; set; }
        public virtual User User { get; set; }
    }
}
