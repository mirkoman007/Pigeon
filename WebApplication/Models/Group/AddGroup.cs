using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Group
{
    public class AddGroup
    {
        public string Name { get; set; }
        public string Decription { get; set; }
        public int CreatorUserId { get; set; }
    }
}
