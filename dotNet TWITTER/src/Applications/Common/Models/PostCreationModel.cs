using System;
using Microsoft.EntityFrameworkCore;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class PostCreationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PostFilling { get; set; }
        public static void Create(PostContext context, string filling)
        {
            context.Posts.AddRange(
                new Post
                {
                    Date = System.DateTime.Now,
                    Filling = filling
                }
            );
            context.SaveChanges();
        }
    }


}
