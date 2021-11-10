using Microsoft.EntityFrameworkCore;
using dotNet_TWITTER.Applications.Common.Models;
using System.Linq;

namespace dotNet_TWITTER.Applications.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> UsersDB { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
