using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Post
{
    public class AddGroupPost
    {

        public string Text { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}
