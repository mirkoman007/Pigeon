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
            PostReactions = new HashSet<PostReaction>();
            PostReports = new HashSet<PostReport>();
        }

        public int Idpost { get; set; }
        public string Text { get; set; }
        public DateTime? DateTime { get; set; }
        public int? GroupId { get; set; }
        public int UserId { get; set; }
        public int? MediaId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Medium Media { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
