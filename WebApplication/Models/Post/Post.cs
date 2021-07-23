using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Post
{
    public class Post
    {
        public int IdPost { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string UserFirstLastName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
