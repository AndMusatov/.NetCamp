using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Infrastructure.Services
{
    public class PostsActions
    {
        private readonly IPostsRepository _postRepository;

        public PostsActions(IPostsRepository postsRepository)
        {
            _postRepository = postsRepository;  
        }

        public async Task<List<Post>> GetAuthUserPosts(string userName, PostsParameters postsParameters)
        {
            var result = await _postRepository.GetAuthPosts(userName);
            return result
                .Skip((postsParameters.PageNumber - 1) * postsParameters.PageSize)
                .Take(postsParameters.PageSize)
                .ToList();
        }

        public async Task<Post> AddPost(string filling, string userName,string userId)
        {
            if (CanCreatePost(filling))
            {
                Post post = new Post
                {
                    PostId = Guid.NewGuid().ToString("N"),
                    UserName = userName,
                    UserId = userId,
                    Date = DateTime.Now,
                    Filling = filling
                };
                await _postRepository.Add(post);
                return post;
            }
            return null;
        }

        public async Task<string> DeletePost(string postId)
        {
            var post = await _postRepository.GetById(postId);
            if (post != null)
            {
                await _postRepository.Remove(post);
                return "This post is deleted";
            }
            return "This post doesn`t exist";
        }

        public bool CanCreatePost(string filling)
        {
            if (string.IsNullOrWhiteSpace(filling))
            {
                return false;
            }
            return true;
        }
        public async Task<List<Post>> GetSubPosts(string userName, PostsParameters postsParameters)
        {
            List<Post> posts = await _postRepository.GetSubPosts(userName);
            posts.Sort((ps1, ps2) => DateTime.Compare(ps1.Date, ps2.Date));
            var result = posts
                .Skip((postsParameters.PageNumber - 1) * postsParameters.PageSize)
                .Take(postsParameters.PageSize);
            return result.ToList();
        }
    }
}
