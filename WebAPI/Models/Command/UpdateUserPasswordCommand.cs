namespace WebAPI.Models.Command
{
    public class UpdateUserPasswordCommand
    {
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
