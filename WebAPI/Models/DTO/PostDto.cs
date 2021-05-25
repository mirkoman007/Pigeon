using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DTO
{
    public class PostDto
    {
        public int IdPost { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; }

        public int? GroupId { get; set; }

        public int? MediaId { get; set; }
    }
}
