using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupReports = new HashSet<GroupReport>();
            Posts = new HashSet<Post>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Idgroup { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateTime { get; set; }

        public virtual ICollection<GroupReport> GroupReports { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
