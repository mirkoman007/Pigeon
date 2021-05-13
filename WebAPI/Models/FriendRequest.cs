using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("FriendRequest")]
    public partial class FriendRequest
    {
        [Key]
        [Column("IDFriendRequest")]
        public int IdfriendRequest { get; set; }
        [Column("UserRequestID")]
        public int? UserRequestId { get; set; }
        [Column("UserResponderID")]
        public int? UserResponderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        [ForeignKey(nameof(UserRequestId))]
        [InverseProperty(nameof(User.FriendRequestUserRequests))]
        public virtual User UserRequest { get; set; }
        [ForeignKey(nameof(UserResponderId))]
        [InverseProperty(nameof(User.FriendRequestUserResponders))]
        public virtual User UserResponder { get; set; }
    }
}
