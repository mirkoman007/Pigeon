namespace WebAPI.Models.Command
{
    public class AddUserGroupCommand
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public string UserType { get; set; }
    }
}
