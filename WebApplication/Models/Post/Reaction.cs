using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Post
{
    public class Reaction
    {
        public int IdPostReaction { get; set; }
        public string ReactionName { get; set; }
        public string FirstLastName { get; set; }
        public int UserID { get; set; }

    }
}
