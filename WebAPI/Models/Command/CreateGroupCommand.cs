using System;

namespace WebAPI.Models.Command
{
    public class CreateGroupCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
