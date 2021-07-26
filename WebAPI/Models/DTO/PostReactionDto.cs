namespace WebAPI.Models.DTO
{
    public class PostReactionDto
    {
        public int IdPostReaction { get; set; }

        public string ReactionName { get; set; }

        public string FirstLastName { get; set; }

        public int? UserID { get; set; }
    }
}
