using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class MessageReport
    {
        public int IdmessageReport { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public DateTime? DateTime { get; set; }
        public int? MessageId { get; set; }
        public int? SenderId { get; set; }

        public virtual Message Message { get; set; }
        public virtual User Sender { get; set; }
    }
}
