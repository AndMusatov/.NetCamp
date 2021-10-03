using System;
using Microsoft.EntityFrameworkCore;

namespace dotNet_TWITTER.Models
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public PostContext(DbContextOptions<PostContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
