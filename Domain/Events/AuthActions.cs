﻿using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Domain.Events
{
    public class AuthActions
    {
        private UserContext _context;
        public AuthActions(UserContext context)
        {
            _context = context;
        }

        public async Task<RegisterModel> Registration(RegisterModel registerModel)
        {
            User user = await _context.UsersDB.FirstOrDefaultAsync(u => u.EMail == registerModel.Email);
            if (user == null)
            {
                _context.UsersDB.Add(new User
                {
                    EMail = registerModel.Email,
                    Password = registerModel.Password,
                    UserName = registerModel.UserName,
                });
                await _context.SaveChangesAsync();

                return registerModel;
            }
            else
                return null;
        }

        public async Task<User> Login(LoginModel loginModel)
        {
            User user = await _context.UsersDB.FirstOrDefaultAsync(u => u.EMail == loginModel.Email && u.Password == loginModel.Password);
            if (user != null)
                return user;
            return null;
        }
    }
}