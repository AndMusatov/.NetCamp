using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Controllers;
using dotNet_TWITTER.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Twitter.Unit.Test
{
    public class PostsTests
    {
        private readonly Mock<IPostsRepository> _postRepository;
        public PostsTests()
        {
            _postRepository = new Mock<IPostsRepository>();
        }

        [Fact]
        public async Task PostCreate_should_return_PostFilling_PostUserId_PostUserName() 
        {
            //Arrange
            var user = CreateUser();
            string fakeUserId = "1";
            string fakeUserName = "UserTest";
            string fakeFilling = "test"; 
            Post post = GetPost(fakeFilling, fakeUserId, fakeUserName);

            //Act
            var postsController = new PostController(_postRepository.Object);
            postsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            ActionResult<Post> actionResult = await postsController.CreatePost(fakeFilling);
            Post result = ((ObjectResult)actionResult.Result).Value as Post;

            //Assert
            result.Filling.Should().Be(post.Filling);
            result.UserId.Should().Be(post.UserId);
            result.UserName.Should().Be(post.UserName);
        }

        [Fact]
        public async Task PostCreate_should_return_BadRequest()
        {
            //Arrange
            var user = CreateUser();
            string fakeFilling = "        ";

            //Act
            var postsController = new PostController(_postRepository.Object);
            postsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var result = await postsController.CreatePost(fakeFilling) as BadRequestResult;

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAuthUserPosts_should_return_ListOfPosts()
        {
            //Arrange
            var user = CreateUser();
            List<Post> posts = CreatePosts();
            PostsParameters postsParameters = new PostsParameters { PageNumber = 1, PageSize = 5};
            _postRepository.Setup(p => p.GetAuthPosts("UserTest")).Returns(Task.FromResult(posts));

            //Act
            var postsController = new PostController(_postRepository.Object);
            postsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            ActionResult<List<Post>> actionResult = await postsController.GetAuthUserPosts(postsParameters);
            List<Post> result = ((ObjectResult)actionResult.Result).Value as List<Post>;

            //Assert
            result.Should().BeEquivalentTo(posts);
        }

        [Fact]
        public async Task GetSubPosts_should_return_ListOfPosts()
        {
            //Arrange
            var user = CreateUser();
            string fakeUserName = "UserTest2";
            List<Post> posts = CreatePosts(fakeUserName: fakeUserName);
            PostsParameters postsParameters = new PostsParameters { PageNumber = 1, PageSize = 5 };
            _postRepository.Setup(p => p.GetSubPosts("UserTest")).Returns(Task.FromResult(posts));

            //Act
            var postsController = new PostController(_postRepository.Object);
            postsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            ActionResult<List<Post>> actionResult = await postsController.GetSubPosts(postsParameters);
            List<Post> result = ((ObjectResult)actionResult.Result).Value as List<Post>;

            //Assert
            result.Should().BeEquivalentTo(posts);
        }

        private Post GetPost(string filling, string fakeUserId = "1", string fakeUserName= "UserTest")
        {
            return new Post
            {
                UserName = fakeUserName,
                UserId = fakeUserId,
                Filling = filling
            };
        }

        private List<Post> CreatePosts(string filling = "test", string fakeUserId = "1", string fakeUserName = "UserTest")
        {
            int postId = 0;
            var result =
            new List<Post>
            {
                new Post
                {
                    PostId = (postId++).ToString(),
                    UserId = fakeUserId,
                    UserName = fakeUserName,
                    Filling = filling
                },
                new Post
                {
                    PostId = (postId++).ToString(),
                    UserId = fakeUserId,
                    UserName = fakeUserName,
                    Filling = filling
                },
                new Post
                {
                    PostId = (postId++).ToString(),
                    UserId = fakeUserId,
                    UserName = fakeUserName,
                    Filling = filling
                }
            };
            return result;
        }

        private ClaimsPrincipal CreateUser()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.NameIdentifier, "1"),
                            new Claim(ClaimTypes.Name, "UserTest"),
                            new Claim(ClaimTypes.Email, "user@gmail.com")
                            //other required and custom claims
                       }, "TestAuthentication"));
        }
    }
}
