using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebAPI.Models
{
    public partial class Medium
    {
        [Key]
        [Column("IDMedia")]
        public int Idmedia { get; set; }
        [Column("PostID")]
        public int? PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("Media")]
        public virtual Post Post { get; set; }
    }
}
