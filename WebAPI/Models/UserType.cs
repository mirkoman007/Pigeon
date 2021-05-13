using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int IduserType { get; set; }
        public string Value { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
