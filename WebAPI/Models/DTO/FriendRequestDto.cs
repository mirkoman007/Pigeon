using System;

namespace WebAPI.Models.DTO
{
    public class FriendRequestDto
    {
        public int IdfriendRequest { get; set; }
        public int UserRequestId { get; set; }
        public int UserResponderId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
