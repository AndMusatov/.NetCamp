using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.DTO;

namespace dotNet_TWITTER.Domain.Events
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

        public string DeleteUser()
        {
            if (LoginStatusDTO.loginStatus.Status)
            {
                Users.RemoveAt(LoginStatusDTO.loginStatus.LoginUser.UserId);
                LoginStatusDTO.loginStatus.LoginUser = null;
                LoginStatusDTO.loginStatus.Status = false;
                return "Ok";
            }
            return "You aren`t logined";
        }

        public List<User> GetAllUsers()
        {
            return Users;
        }
    }
}
