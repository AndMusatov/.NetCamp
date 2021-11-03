using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.DTO;

namespace dotNet_TWITTER.Domain.Events
{
    public class MainPostsActions
    {
        private UserContext _context;
        public List<Post> Posts = new List<Post>();

        public MainPostsActions(UserContext context)
        {
            _context = context;
        }

        public Post GetPost(int i)
        {
            return Posts[i];
        }

        public bool PostInitCheck()
        {
            if (Posts.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckLoginStatus()
        {
            return LoginStatusDTO.loginStatus.Status;
        }

        public string AddPost(string filling, string userName)
        {
            if (CanCreatePost(filling))
            {
                User user = _context.UsersDB.FirstOrDefault(u => u.UserName == userName);
                if (user.UserPosts == null)
                {
                    _context.UsersDB.FirstOrDefault(u => u.UserName == userName).UserPosts = new List<Post>();
                }
                _context.UsersDB.FirstOrDefault(u => u.UserName == userName).UserPosts.Add(
                new Post
                {
                    UserName = "avc",
                    Id = _context.UsersDB.FirstOrDefault(u => u.UserName == userName).UserPosts.Count,
                    Date = DateTime.Now,
                    Filling = filling
                }
                );
                _context.SaveChangesAsync();
                return ("Input is Ok");
            }
            return "Input is wrong";
        }

        public void DeletePost(int postId)
        {
            Posts.RemoveAt(postId);
        }

        public bool CanCreatePost(string filling)
        {
            if (string.IsNullOrEmpty(filling))
            {
                return false;
            }
            return true;
        }
    }
}
