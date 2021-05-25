using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Reaction
    {
        public Reaction()
        {
            CommentReactions = new HashSet<CommentReaction>();
            MessageReactions = new HashSet<MessageReaction>();
            PostReactions = new HashSet<PostReaction>();
        }

        public int Idreaction { get; set; }
        public string Value { get; set; }

        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
    }
}
