namespace WebAPI.Models.Command
{
    public class SendMessageCommand
    {
        public string Text { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string SenderFirstLastName { get; set; }

        public string ReceiverFirstLastname { get; set; }
    }
}
