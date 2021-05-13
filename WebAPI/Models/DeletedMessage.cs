using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class DeletedMessage
    {
        public int IddeletedMessage { get; set; }
        public DateTime? DateTime { get; set; }
        public int? MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
