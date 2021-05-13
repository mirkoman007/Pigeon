using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class PostReaction
    {
        public int IdpostReaction { get; set; }
        public int? PostId { get; set; }
        public int? ReactionId { get; set; }
        public int? UserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Reaction Reaction { get; set; }
        public virtual User User { get; set; }
    }
}
