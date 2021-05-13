using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    [Table("Post")]
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Media = new HashSet<Medium>();
            PostReactions = new HashSet<PostReaction>();
            PostReports = new HashSet<PostReport>();
        }

        [Key]
        [Column("IDPost")]
        public int Idpost { get; set; }
        [StringLength(255)]
        public string Text { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
        [Column("GroupID")]
        public int? GroupId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("Posts")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Posts")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(Comment.Post))]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty(nameof(Medium.Post))]
        public virtual ICollection<Medium> Media { get; set; }
        [InverseProperty(nameof(PostReaction.Post))]
        public virtual ICollection<PostReaction> PostReactions { get; set; }
        [InverseProperty(nameof(PostReport.Post))]
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
