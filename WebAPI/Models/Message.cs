using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Message
    {
        public Message()
        {
            DeletedMessages = new HashSet<DeletedMessage>();
            MessageReactions = new HashSet<MessageReaction>();
            MessageReports = new HashSet<MessageReport>();
        }

        public int Idmessage { get; set; }
        public string Text { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? SeenDateTime { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
        public virtual User Sender { get; set; }
        public virtual ICollection<DeletedMessage> DeletedMessages { get; set; }
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        public virtual ICollection<MessageReport> MessageReports { get; set; }
    }
}
