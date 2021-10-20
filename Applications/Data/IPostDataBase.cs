using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Applications.Data
{
    public class IPostDataBase
    {
        public static List<Post> PostsDB { get; set; }
        public IPostDataBase()
        {
            PostsDB = new List<Post>
            {
                new Post
                {
                    Id = 0,
                    Filling = "FirstPost",
                    Date = DateTime.Now,
                    UserName = "User0"
                },
                new Post
                {
                    Id = 1,
                    Filling = "SecondPost",
                    Date = DateTime.Now,
                    UserName = "User0"
                }
            };
        }

        public static List<Post> GetPostsList()
        {
            return PostsDB;
        }

        public static void SetPostsList(List<Post> posts)
        {
            PostsDB = posts;
        }
    }
}
