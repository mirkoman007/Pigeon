using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Media = new HashSet<Medium>();
            PostReactions = new HashSet<PostReaction>();
            PostReports = new HashSet<PostReport>();
        }

        public int Idpost { get; set; }
        public string Text { get; set; }
        public string PostPlace { get; set; }
        public DateTime? DateTime { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Medium> Media { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
