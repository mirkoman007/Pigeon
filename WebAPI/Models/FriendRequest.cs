using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class FriendRequest
    {
        public int IdfriendRequest { get; set; }
        public int UserRequestId { get; set; }
        public int UserResponderId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual User UserRequest { get; set; }
        public virtual User UserResponder { get; set; }
    }
}
