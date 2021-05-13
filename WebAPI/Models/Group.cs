using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Group")]
    public partial class Group
    {
        public Group()
        {
            GroupReports = new HashSet<GroupReport>();
            Posts = new HashSet<Post>();
            UserGroups = new HashSet<UserGroup>();
        }

        [Key]
        [Column("IDGroup")]
        public int Idgroup { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        [InverseProperty(nameof(GroupReport.Group))]
        public virtual ICollection<GroupReport> GroupReports { get; set; }
        [InverseProperty(nameof(Post.Group))]
        public virtual ICollection<Post> Posts { get; set; }
        [InverseProperty(nameof(UserGroup.Group))]
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
