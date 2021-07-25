using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Post
{
    public class AddReaction
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string ReactionName { get; set; }
    }
}
