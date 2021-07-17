using System;

namespace WebAPI.Models.DTO
{
    public class MessageUsersDto
    {
        public int? UserId { get; set; }

        public string UserFirstLastname { get; set; }

        public string LastMessageText { get; set; }

        public DateTime? LastMessageDateTime { get; set; }


    }
}
