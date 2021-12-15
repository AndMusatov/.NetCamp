using dotNet_TWITTER.Applications.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using EFCore.BulkExtensions;

namespace dotNet_TWITTER.Infrastructure.Services
{
    public class DataGenerator
    {
        private readonly UserManager<User> _userManager;
        private UserContext _context;
        private static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private static CancellationToken token;

        public DataGenerator(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<object> Generate(int quantityOfUsers, int quantityOfPostsForOneUser)
        {
            var faker = new Faker();
            token = cancelTokenSource.Token;

            var userIds = _userManager.Users.Count() + 1;

            var testUsers = new Faker<User>()
                .CustomInstantiator(f => new User())
                .RuleFor(o => o.Id, f => (userIds++).ToString())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email());

            List<IdentityResult> responses = new List<IdentityResult>(); 
            for (int i = 0; i < quantityOfUsers; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return responses;
                }
                User user = testUsers.Generate();
                var response = await _userManager.CreateAsync(user, "!Aa1111");
                responses.Add(response);
                List<Post> posts = new List<Post>();
                for (int j = 0; j < quantityOfPostsForOneUser; j++)
                {
                    posts.Add(GeneratePost(user, Guid.NewGuid().ToString("N")));
                    if (token.IsCancellationRequested)
                    {
                        return responses;
                    }
                }
                _context.Post.AddRange(posts);
                await _context.SaveChangesAsync();
            }
            return responses;
        }

        public async Task<Object> AddBulkDataAsync(int quantityOfUsers, int quantityOfPostsForOneUser)
        {
            var faker = new Faker();
            token = cancelTokenSource.Token;

            var userIds = _userManager.Users.Count() + 1;

            var testUsers = new Faker<User>()
                .CustomInstantiator(f => new User())
                .RuleFor(o => o.Id, f => (userIds++).ToString())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email());

            List<IdentityResult> responses = new List<IdentityResult>();
            for (int i = 0; i < quantityOfUsers; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return responses;
                }
                User user = testUsers.Generate();
                var response = await _userManager.CreateAsync(user, "!Aa1111");
                responses.Add(response);
                List<Post> posts = new List<Post>();
                for (int j = 0; j < quantityOfPostsForOneUser; j++)
                {
                    posts.Add(GeneratePost(user, Guid.NewGuid().ToString("N")));
                    if (token.IsCancellationRequested)
                    {
                        return responses;
                    }
                }
                await _context.BulkInsertAsync(posts);
            }
            return responses;
        }

        public static Post GeneratePost(User user, string postId)
        {
            var faker = new Faker();

            string randomString = faker.Random.Words();
            var testPosts = new Faker<Post>()
                .StrictMode(false)
                .RuleFor(p => p.Filling, randomString)
                .RuleFor(p => p.Date, f => f.Date.Past())
                .RuleFor(p => p.User, user)
                .RuleFor(p => p.UserId, user.Id)
                .RuleFor(p => p.UserName, user.UserName)
                .RuleFor(p => p.PostId, postId);

            return testPosts.Generate();
        }

        public static void CancellGeneration()
        {
            token = cancelTokenSource.Token;
            cancelTokenSource.Cancel(); 
        }
    }
}
