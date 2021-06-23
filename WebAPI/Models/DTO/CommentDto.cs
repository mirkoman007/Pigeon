using System;

namespace WebAPI.Models.DTO
{
    public class CommentDto
    {
        public int IDComment { get; set; }

        public string Text { get; set; }

        public int CommentId { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public string UserFirstLastName { get; set; }

    }
}
