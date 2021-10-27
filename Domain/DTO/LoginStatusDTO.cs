using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Domain.DTO
{
    public class LoginStatusDTO
    {
        public static LoginStatus loginStatus { get; set; }
        public LoginStatusDTO()
        {
            loginStatus = new LoginStatus
            {
                Status = false,
                LoginUser = null
            };
        }
    }
}
