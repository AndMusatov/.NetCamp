using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Applications.Data
{
    public class MainPostsActions
    {
        public List<Post> Posts = new List<Post>();

        public MainPostsActions()
        {
            Posts = IPostDataBase.GetPostsList();
        }

        public Post GetPost(int i)
        {
            if (Posts.Count >= i)
            {
                return Posts[i];
            }
            return null;
        }

        public List<Post> GetPosts()
        {
            return Posts;
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

        public string AddPost(string filling)
        {
            if (LoginStatusDTO.loginStatus.Status)
            {
                if (LoginStatusDTO.loginStatus.LoginUser.UserPosts == null)
                {
                    LoginStatusDTO.loginStatus.LoginUser.UserPosts = new List<Post>();
                }
                if (CanCreatePost(filling))
                {
                    Posts = LoginStatusDTO.loginStatus.LoginUser.UserPosts;
                    Posts.Add(
                    new Post
                    {
                        UserName = LoginStatusDTO.loginStatus.LoginUser.UserName,
                        Id = Posts.Count,
                        Date = DateTime.Now,
                        Filling = filling
                    }
                    );
                    IUserDataBase.SetUsersPosts(Posts, LoginStatusDTO.loginStatus.LoginUser.UserId);
                    return "Input is Ok";
                }
                return "Input is wrong";
            }
            else
            {
                return "You aren`t logined";
            }
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
