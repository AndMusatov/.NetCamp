using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class User : IdentityUser
    {
        public static object Identity { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<Post> UserPosts { get; set; }
        public List<Comment> UserComments { get; set; }
    }
}
