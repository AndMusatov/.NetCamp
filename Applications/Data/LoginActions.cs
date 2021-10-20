using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Applications.Data
{
    public class LoginActions
    {
        List<User> Users = new List<User>();
        public string Login(string userName, string password)
        {
            Users = IUserDataBase.GetUsers();
            foreach (var User in Users)
            {
                if (User.UserName == userName)
                {
                    if (User.Password == password)
                    {
                        LoginStatusDTO.loginStatus.Status = true;
                        LoginStatusDTO.loginStatus.LoginUser = User;
                        return "Login is Ok";
                    }
                }
            }
            return "Login is Wrong";
        }

        public LoginStatus CheckLoginStatus()
        {
            return LoginStatusDTO.loginStatus;
        }
    }
}
