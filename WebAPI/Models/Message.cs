using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Message")]
    public partial class Message
    {
        public Message()
        {
            DeletedMessages = new HashSet<DeletedMessage>();
            MessageReactions = new HashSet<MessageReaction>();
            MessageReports = new HashSet<MessageReport>();
        }

        [Key]
        [Column("IDMessage")]
        public int Idmessage { get; set; }
        [StringLength(255)]
        public string Text { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SeenDateTime { get; set; }
        [Column("SenderID")]
        public int? SenderId { get; set; }
        [Column("ReceiverID")]
        public int? ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        [InverseProperty(nameof(User.MessageReceivers))]
        public virtual User Receiver { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.MessageSenders))]
        public virtual User Sender { get; set; }
        [InverseProperty(nameof(DeletedMessage.Message))]
        public virtual ICollection<DeletedMessage> DeletedMessages { get; set; }
        [InverseProperty(nameof(MessageReaction.Message))]
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
        [InverseProperty(nameof(MessageReport.Message))]
        public virtual ICollection<MessageReport> MessageReports { get; set; }
    }
}
