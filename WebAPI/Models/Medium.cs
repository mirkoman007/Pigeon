using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Medium
    {
        public int Idmedia { get; set; }
        public int? PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
