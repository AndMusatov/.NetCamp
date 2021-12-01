using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Infrastructure.Repository;
using dotNet_TWITTER.WEB_UI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Twitter.Unit.Test
{
    public class PostsTests
    {
        private readonly Mock<IPostsRepository> _postRepository;
        private readonly Mock<IGenericRepository<Post>> _genericRepository;
        private readonly Mock<ILogger<PostController>> _loggerMock;
        public PostsTests()
        {
            _postRepository = new Mock<IPostsRepository>();
            _genericRepository = new Mock<IGenericRepository<Post>>();
            _loggerMock = new Mock<ILogger<PostController>>();
        }
        
        [Fact]
        public async Task PostCreate_should_return_True()
        {
            //Arrange

            string fakeUserId = "2";
            string fakeUserName = "UserTest";
            string filling = "test";
            Post post = GetPost(fakeUserId, fakeUserName, filling);

            //Act
            var postsController = new PostController(_postRepository.Object, _genericRepository.Object);
            var actionResult = await postsController.CreatePost(filling);

            //Assert
            actionResult.Should().Be(post);
        }

        private Post GetPost(string fakeUserId, string fakeUserName, string filling)
        {
            return new Post
            {
                UserName = fakeUserName,
                UserId = fakeUserId,
                Filling = filling
            };
        }
    }
}
