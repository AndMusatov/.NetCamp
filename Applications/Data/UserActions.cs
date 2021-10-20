using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Applications.Data
{
    public class UserActions
    {
        public static List<User> Users { get; set; }
        public UserActions()
        {
            Users = IUserDataBase.GetUsers();
        }

        public User GetUser(int id)
        {
            return Users[id];
        }

        public string NewUser(string username, string password, string mailadress)
        {
            Users.Add(
                new User
                {
                    UserId = Users.Count(),
                    UserName = username,
                    Password = password,
                    MailAdress = mailadress
                }
                );
            IUserDataBase.SetUsers(Users);
            return "Input is Ok";
        }

        public List<User> GetAllUsers()
        {
            return Users;
        }
    }
}
