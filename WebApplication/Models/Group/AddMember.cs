using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Group
{
    public class AddMember
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string UserType { get; set; }
    }
}
