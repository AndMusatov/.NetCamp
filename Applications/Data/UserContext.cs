using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotNet_TWITTER.Applications.Data
{
    public class UserContext : IdentityDbContext<User>
    {
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.UserPosts)
                .HasForeignKey(p => p.UserName)
                .HasPrincipalKey(u => u.UserName);
            modelBuilder.Entity<Subscription>()
               .HasOne(s => s.user)
               .WithMany(u => u.Subscriptions)
               .HasForeignKey(s => s.AuthUser)
               .HasPrincipalKey(u => u.UserName);
            modelBuilder.Entity<Comment>()
               .HasOne(c => c.post)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.PostId)
               .HasPrincipalKey(p => p.PostId);
            base.OnModelCreating(modelBuilder);
        }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
