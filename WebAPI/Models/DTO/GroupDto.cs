using System;

namespace WebAPI.Models.DTO
{
    public class GroupDto
    {
        public int IDGroup { get; set; }

        public string Name { get; set; }

        public string Decription { get; set; }

        public DateTime? CreatedDateTime { get; set; }
    }
}
