using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class User : IdentityUser
    {
        [Key]
        public static object Identity { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<Post> UserPosts { get; set; }
        public List<Comment> UserComments { get; set; }
    }
}
