using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Group
{
    public class SearchGroup
    {
        public int IdGroup { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
