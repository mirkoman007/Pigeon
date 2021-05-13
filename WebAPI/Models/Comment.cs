﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Comment
    {
        public Comment()
        {
            CommentReactions = new HashSet<CommentReaction>();
            CommentReports = new HashSet<CommentReport>();
        }

        public int Idcomment { get; set; }
        public string Text { get; set; }
        public DateTime? DateTime { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CommentReaction> CommentReactions { get; set; }
        public virtual ICollection<CommentReport> CommentReports { get; set; }
    }
}
