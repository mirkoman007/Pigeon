using System;

namespace WebAPI.Models.DTO
{
    public class WarningDto
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public string Explanation { get; set; }

        public DateTime? DateTime { get; set; }

        public int UserId { get; set; }

        public string UserFirstLastName { get; set; }

        public int AdminId { get; set; }

        public string AdminFirstLastName { get; set; }
    }
}
