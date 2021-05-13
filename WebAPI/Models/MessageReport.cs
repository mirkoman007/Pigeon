using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("MessageReport")]
    public partial class MessageReport
    {
        [Key]
        [Column("IDMessageReport")]
        public int IdmessageReport { get; set; }
        [StringLength(255)]
        public string Reason { get; set; }
        [StringLength(255)]
        public string Explanation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("MessageID")]
        public int? MessageId { get; set; }
        [Column("SenderID")]
        public int? SenderId { get; set; }

        [ForeignKey(nameof(MessageId))]
        [InverseProperty("MessageReports")]
        public virtual Message Message { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.MessageReports))]
        public virtual User Sender { get; set; }
    }
}
