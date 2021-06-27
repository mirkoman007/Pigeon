using System;

namespace WebAPI.Models.DTO
{
    public class FriendRequestDto
    {
        public int IdfriendRequest { get; set; }
        public int UserRequestId { get; set; }
        public string UserRequestName { get; set; }
        public int UserResponderId { get; set; }
        public string UserResponderName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
