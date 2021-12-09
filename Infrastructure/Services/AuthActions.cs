using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Services
{
    public class AuthActions
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostsRepository _postsRepository;
        public AuthActions(IUserRepository userRepository, IPostsRepository postsRepository)
        {
            _userRepository = userRepository;
            _postsRepository = postsRepository;
        }

        public async Task<object> Registration(RegisterModel registerModel)
        {
            User user = new User { Email = registerModel.Email, UserName = registerModel.UserName };
            var result = await _userRepository.RegisterUser(user, registerModel.Password);
            await _userRepository.SignInUser(user);
            return result;
        }

        public async Task<object> DeleteUser(string userId)
        {
            User user = await _userRepository.GetById(userId);
            await _postsRepository.RemoveUserPosts(user.UserName);
            await _userRepository.Remove(user);
            return "User was deleted";
        }

        public async Task<object> GoogleRegister(string email, string userName)
        {
            User user = new User
            {
                Email = email,
                UserName = userName
            };
            return await _userRepository.GoogleRegisterUser(user);
        }

        public async Task GoogleLogin(string email)
        {
            User user = await _userRepository.GetByEmail(email);
            await _userRepository.SignInUser(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }
    }
}
