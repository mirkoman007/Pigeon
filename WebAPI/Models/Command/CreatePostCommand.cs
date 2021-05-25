namespace WebAPI.Models.Command
{
    public class CreatePostCommand
    {
        public string Text { get; set; }

        public int? GroupId { get; set; }

        public int? UserId { get; set; }

        public string MediaPath { get; set; }
    }
}
