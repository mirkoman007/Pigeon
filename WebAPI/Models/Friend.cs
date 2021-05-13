using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Friend")]
    public partial class Friend
    {
        [Key]
        [Column("IDFriend")]
        public int Idfriend { get; set; }
        [Column("UserRequestID")]
        public int? UserRequestId { get; set; }
        [Column("UserResponderID")]
        public int? UserResponderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        [ForeignKey(nameof(UserRequestId))]
        [InverseProperty(nameof(User.FriendUserRequests))]
        public virtual User UserRequest { get; set; }
        [ForeignKey(nameof(UserResponderId))]
        [InverseProperty(nameof(User.FriendUserResponders))]
        public virtual User UserResponder { get; set; }
    }
}
