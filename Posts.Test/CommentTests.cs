using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Controllers;
using dotNet_TWITTER.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Twitter.Unit.Test
{
    public class CommentTests
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        public CommentTests()
        {
            _commentRepository = new Mock<ICommentRepository>();
        }

        [Fact]
        public async Task CommentCreate_should_return_CommentFilling_CommentUserName()
        {
            //Arrange
            var user = CreateUser();
            string fakePostId = "1";
            string fakeUserName = "UserTest";
            string fakeFilling = "test";
            Comment comment = CreateComment(fakeFilling, fakeUserName, fakePostId);
            _commentRepository.Setup(p => p.PostForCommentExists("1")).Returns(true);

            //Act
            var commentController = new CommentController(_commentRepository.Object);
            commentController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            ActionResult<Comment> actionResult = await commentController.CreatePostComment(fakePostId, fakeFilling);
            Comment result = ((ObjectResult)actionResult.Result).Value as Comment;

            //Assert
            result.CommentFilling.Should().Be(comment.CommentFilling);
            result.UserName.Should().Be(comment.UserName);
        }

        [Fact]
        public async Task CommentCreate_With_Wrong_Input_should_return_BadRequest()
        {
            //Arrange
            var user = CreateUser();
            string fakePostId = "1";
            string fakeFilling = "       ";
            _commentRepository.Setup(p => p.PostForCommentExists("1")).Returns(true);

            //Act
            var commentController = new CommentController(_commentRepository.Object);
            commentController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var result = await commentController.CreatePostComment(fakePostId, fakeFilling) as BadRequestResult;

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CommentCreate_Without_Post_should_return_BadRequest()
        {
            //Arrange
            var user = CreateUser();
            string fakePostId = "100000000";
            string fakeFilling = "Correct Input";
            _commentRepository.Setup(p => p.PostForCommentExists("100000000")).Returns(false);

            //Act
            var commentController = new CommentController(_commentRepository.Object);
            commentController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var result = await commentController.CreatePostComment(fakePostId, fakeFilling) as BadRequestResult;

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetPostComment_should_return_ListOfComments()
        {
            //Arrange
            var user = CreateUser();
            List<Comment> comments = CreateComments();
            _commentRepository.Setup(p => p.GetPostComments("1")).Returns(Task.FromResult(comments));
            _commentRepository.Setup(p => p.PostForCommentExists("1")).Returns(true);

            //Act
            var commentController = new CommentController(_commentRepository.Object);
            commentController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            ActionResult<List<Comment>> actionResult = await commentController.GetPostComments("1");
            List<Comment> result = ((ObjectResult)actionResult.Result).Value as List<Comment>;

            //Assert
            result.Should().BeEquivalentTo(comments);
        }

        [Fact]
        public async Task GetPostComment_Without_Post_should_return_BadRequest()
        {
            //Arrange
            var user = CreateUser();
            List<Comment> comments = CreateComments();
            _commentRepository.Setup(p => p.GetPostComments("1")).Returns(Task.FromResult(comments));
            _commentRepository.Setup(p => p.PostForCommentExists("100000000")).Returns(false);

            //Act
            var commentController = new CommentController(_commentRepository.Object);
            commentController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var result = await commentController.GetPostComments("100000000") as BadRequestResult;

            //Assert
            result.Should().NotBeNull();
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

        private Comment CreateComment(string filling = "test", string userName = "UserTest", string postId = "1")
        {
            return new Comment
            {
                CommentFilling = filling,
                UserName = userName,
                PostId = postId
            };
        }

        private List<Comment> CreateComments(string filling = "test", string fakeUserName = "UserTest", string postId = "1")
        {
            int commentId = 0;
            var result =
            new List<Comment>
            {
                new Comment
                {
                    PostId = postId,
                    CommentId = (commentId++).ToString(),
                    UserName = fakeUserName,
                    CommentFilling = filling
                },
                new Comment
                {
                    PostId = postId,
                    CommentId = (commentId++).ToString(),
                    UserName = fakeUserName,
                    CommentFilling = filling
                },
                new Comment
                {
                    PostId = postId,
                    CommentId = (commentId++).ToString(),
                    UserName = fakeUserName,
                    CommentFilling = filling
                }
            };
            return result;
        }
    }
}
