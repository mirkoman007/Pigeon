namespace WebAPI.Models.Command
{
    public class WarningCommand
    {
        public string Reason { get; set; }

        public string Explanation { get; set; }

        public int UserId { get; set; }

        public int AdminId { get; set; }
    }
}
