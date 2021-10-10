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
                    Id = 1,
                    Filling = "FirstPost",
                    Date = DateTime.Now
                },
                new Post
                {
                    Id = 2,
                    Filling = "SecondPost",
                    Date = DateTime.Now
                }
            };
        }

        public static Post Send(int i)
        {
            return PostsDB[i];
        }

        public bool PostInitCheck()
        {
            if (PostsDB.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool CreateTest(string filling)
        {
            if (string.IsNullOrEmpty(filling))
            {
                return false;
            }
            Create(filling);
            if (PostsDB.Count == 3)
            {
                return true;
            }
            return false;
        }
        public void Create(string filling)
        {
            PostsDB.Add(
                new Post
                {
                    Id = PostsDB.Count + 1,
                    Date = DateTime.Now,
                    Filling = filling
                }
                );
        }
    }
}
