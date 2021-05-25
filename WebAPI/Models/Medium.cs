using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Medium
    {
        public Medium()
        {
            Posts = new HashSet<Post>();
        }

        public int Idmedia { get; set; }
        public string Title { get; set; }
        public string MediaPath { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
