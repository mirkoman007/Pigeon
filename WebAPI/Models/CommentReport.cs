using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class CommentReport
    {
        public int IdcommentReport { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public DateTime? DateTime { get; set; }
        public int? CommentId { get; set; }
        public int? SenderId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User Sender { get; set; }
    }
}
