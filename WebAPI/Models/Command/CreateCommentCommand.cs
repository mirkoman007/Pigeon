namespace WebAPI.Models.Command
{
    public class CreateCommentCommand
    {
        public string Text { get; set; }

        public int CommentId { get; set; }

        public int? UserId { get; set; }
    }
}
