using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Applications.Data
{
    public class IUserDataBase
    {
        public static List<User> UsersDB { get; set; }
        public IUserDataBase()
        {
            UsersDB = new List<User>
            {
                new User
                {
                    Id = 0,
                    UserName = "User0",
                    Password = "1111",
                    EMail = "user@mail.com",
                    UserPosts = IPostDataBase.GetPostsList()
                }
            };
        }

        public static List<User> GetUsers()
        {
            return UsersDB;
        }

        public static void SetUsers(List<User> users)
        {
            UsersDB = users;
        }

        public static void SetUsersPosts(List<Post> posts, int userId)
        {
            UsersDB[userId].UserPosts = posts;
        }
    }
}
