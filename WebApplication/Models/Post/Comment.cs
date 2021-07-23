﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Post
{
    public class Comment
    {
        public int IdComment { get; set; }
        public string Text { get; set; }
        public int CommentId { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public string UserFirstLastName { get; set; }
    }
}
