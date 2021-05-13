using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Warning
    {
        public int Idwarning { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public DateTime? DateTime { get; set; }
        public int? UserId { get; set; }
        public int? AdminId { get; set; }

        public virtual User Admin { get; set; }
        public virtual User User { get; set; }
    }
}
