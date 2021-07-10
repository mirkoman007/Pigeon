using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Friends
{
    public class FriendRequest
    {
        public int IdfriendRequest { get; set; }
        public int UserRequestId { get; set; }
        public string UserRequestName { get; set; }
        public int UserResponderId { get; set; }
        public string UserResponderName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
