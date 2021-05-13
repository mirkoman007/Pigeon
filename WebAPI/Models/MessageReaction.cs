using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class MessageReaction
    {
        public int IdmessageReaction { get; set; }
        public int? MessageId { get; set; }
        public int? ReactionId { get; set; }
        public int? UserId { get; set; }

        public virtual Message Message { get; set; }
        public virtual Reaction Reaction { get; set; }
        public virtual User User { get; set; }
    }
}
