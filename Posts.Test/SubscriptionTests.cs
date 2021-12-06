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
    public class SubscriptionTests
    {
        private readonly Mock<ISubRepository> _subRepository;
        public SubscriptionTests()
        {
            _subRepository = new Mock<ISubRepository>();
        }

        [Fact]
        public async Task UserSubscribe_should_return_SubscriptionUserId_SubscriptionAuthUser_SubscriptionSubUser()
        {
            //Arrange
            var user = CreateUser();
            string fakeSubUsername = "UserTest2";
            Subscription subscription = GetSubscription(fakeSubUserName: fakeSubUsername);
            _subRepository.Setup(p => p.SubUserExists(fakeSubUsername)).Returns(Task.FromResult(true));
            _subRepository.Setup(p => p.SubscriptionExists("UserTest", fakeSubUsername)).Returns(true);

            //Act
            var subController = new SubscriptionController(_subRepository.Object);
            subController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            ActionResult<Subscription> actionResult = await subController.Subscribe(fakeSubUsername);
            Subscription result = ((ObjectResult)actionResult.Result).Value as Subscription;

            //Assert
            result.userId.Should().Be(subscription.userId);
            result.AuthUser.Should().Be(subscription.AuthUser);
            result.SubUser.Should().Be(subscription.SubUser);
        }

        [Fact]
        public async Task UserSubscribe_With_Wrong_SubUserName_should_return_BadRequest()
        {
            //Arrange
            var user = CreateUser();
            string fakeSubUsername = "UserTest2";
            _subRepository.Setup(p => p.SubUserExists(fakeSubUsername)).Returns(Task.FromResult(false));
            _subRepository.Setup(p => p.SubscriptionExists("UserTest", fakeSubUsername)).Returns(true);

            //Act
            var subController = new SubscriptionController(_subRepository.Object);
            subController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var result = await subController.Subscribe(fakeSubUsername) as BadRequestResult;

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UserSubscribe_When_Subscription_Already_Exists_should_return_BadRequest()
        {
            //Arrange
            var user = CreateUser();
            string fakeSubUsername = "UserTest2";
            _subRepository.Setup(p => p.SubUserExists(fakeSubUsername)).Returns(Task.FromResult(true));
            _subRepository.Setup(p => p.SubscriptionExists("UserTest", fakeSubUsername)).Returns(false);

            //Act
            var subController = new SubscriptionController(_subRepository.Object);
            subController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            var result = await subController.Subscribe(fakeSubUsername) as BadRequestResult;

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShowSubscriptions_should_return_ListOfSubscriptions()
        {
            //Arrange
            var user = CreateUser();
            List<Subscription> subscriptions = CreateSubscriptions();
            _subRepository.Setup(p => p.GetAuthSubscriptions("UserTest")).Returns(Task.FromResult(subscriptions));

            //Act
            var subController = new SubscriptionController(_subRepository.Object);
            subController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            ActionResult<List<Subscription>> actionResult = await subController.ShowSubscriptions();
            List<Subscription> result = ((ObjectResult)actionResult.Result).Value as List<Subscription>;

            //Assert
            result.Should().BeEquivalentTo(subscriptions);
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

        private Subscription GetSubscription(string fakeUserName = "UserTest", string fakeSubUserName = "UserTest2", string fakeUserId = "1")
        {
            return new Subscription
            {
                AuthUser = fakeUserName,
                SubUser = fakeSubUserName,
                userId = fakeUserId
            };
        }

        private List<Subscription> CreateSubscriptions(string fakeUserName = "UserTest", string fakeSubUserName = "UserTest2", string fakeUserId = "1")
        {
            int subId = 0;
            var result =
            new List<Subscription>
            {
                new Subscription
                {
                    SubscriptionId = (subId++).ToString(),
                    userId = fakeUserId,
                    AuthUser = fakeUserName,
                },
                new Subscription
                {
                    SubscriptionId = (subId++).ToString(),
                    userId = fakeUserId,
                    AuthUser = fakeUserName,
                },
                new Subscription
                {
                    SubscriptionId = (subId++).ToString(),
                    userId = fakeUserId,
                    AuthUser = fakeUserName,
                }
            };
            return result;
        }
    }
}
