using System;
using System.Collections.Generic;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class Post
    {
        public string PostId { get; set; }
        public string UserName { get; set; }
        public string Filling { get; set; }
        public DateTime Date { get; set; }
        public List<Comment> Comments = new List<Comment>();

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
