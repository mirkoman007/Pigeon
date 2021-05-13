using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class PostReport
    {
        public int IdpostReport { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public DateTime? DateTime { get; set; }
        public int? PostId { get; set; }
        public int? SenderId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User Sender { get; set; }
    }
}
