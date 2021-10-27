using System.Collections.Generic;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MailAdress { get; set; }
        public List<Post> UserPosts { get; set; }
        public List<Comment> UserComments { get; set; }
    }
}
