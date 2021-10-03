using System.Linq;
using dotNet_TWITTER.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNet_TWITTER
{
    public static class SampleData
    {
        public static void Initialize(PostContext context)
        {
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Date = System.DateTime.Now,
                        Filling = "first post"
                    },
                    new Post
                    {
                        Date = System.DateTime.Now,
                        Filling = "second post"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
