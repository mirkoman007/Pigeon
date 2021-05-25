using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class GroupReport
    {
        public int IdgroupReport { get; set; }
        public string Reason { get; set; }
        public string Explanation { get; set; }
        public DateTime? DateTime { get; set; }
        public int? GroupId { get; set; }
        public int? UserSenderId { get; set; }
        public bool? Approved { get; set; }

        public virtual Group Group { get; set; }
        public virtual User UserSender { get; set; }
    }
}
