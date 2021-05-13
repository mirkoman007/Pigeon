using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class UserGroup
    {
        public int IduserGroup { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public string UserType { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
