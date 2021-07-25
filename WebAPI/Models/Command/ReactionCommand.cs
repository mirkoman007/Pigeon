namespace WebAPI.Models.Command
{
    public class ReactionCommand
    {
        public int PostID { get; set; }

        public int UserID { get; set; }

        public string ReactionName { get; set; }
    }
}
