using System;

namespace WebAPI.Models.DTO
{
    public class MessageDto
    {
        public int IDMessage { get; set; }

        public string Text { get; set; }

        public DateTime? DateTime { get; set; }

        public DateTime? SeenDateTime { get; set; }

        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public string SenderFirstLastName { get; set; }

        public string ReceiverFirstLastName { get; set; }
    }
}
