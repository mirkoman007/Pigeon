using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("DeletedMessage")]
    public partial class DeletedMessage
    {
        [Key]
        [Column("IDDeletedMessage")]
        public int IddeletedMessage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("MessageID")]
        public int? MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        [InverseProperty("DeletedMessages")]
        public virtual Message Message { get; set; }
    }
}
