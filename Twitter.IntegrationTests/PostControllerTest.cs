using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using dotNet_TWITTER.Domain.Events;
using FluentAssertions;
using System.Net;
using dotNet_TWITTER.Applications.Common.Models;
using System.Net.Http;
using NUnit.Framework;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Filters;
using dotNet_TWITTER.WEB_UI.Controllers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Twitter.IntegrationTests
{
    class PostControllerTest : IntegrationTest
    {
        [Test]
        public async Task ShowAllPosts_WithoutAnyPosts_ShouldReturnsEmptyResponse()
        {
            //Arrange

            //Act
            var response = await testClient.GetAsync("/ShowAllPosts");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.As<List<Post>>().Should().BeNull();
        }

        [Test]
        public async Task ShowAuthUserPosts_ShouldReturnNull()
        {
            //Arrange
            //await AuthenticateAsync();
            /*var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                Mock.Of<Controller>());

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, Mock.Of<Controller>());
                return Task.FromResult(ctx);
            };

            FakeUserFilter fakeUserFilter = new FakeUserFilter();

            await fakeUserFilter.OnActionExecutionAsync(context, next);*/
            RegisterModel registerModel = new RegisterModel
            {
                Email = "test@gmail.com",
                Password = "1111",
                ConfirmPassword = "1111",
                UserName = "testUser"
            };
            var stringPayload = JsonConvert.SerializeObject(registerModel);
            var stringContent = new StringContent(stringPayload);
            //Act
            CancellationToken cancelTokenSource = new CancellationToken();
            var login = await testClient.PostAsync("/Registration", stringContent, cancelTokenSource);
            var response = await testClient.GetAsync("/ShowAuthUserPosts");


            //Assert
            login.StatusCode.Should().Be(HttpStatusCode.OK);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.As<List<Post>>().Should().BeNull();

        }

        [Test]
        public async Task CreatePost_CreationOfPost_ShouldReturnCreatedPostFromDataBase()
        {
            //Arrange
            //await AuthenticateAsync();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<Microsoft.AspNetCore.Routing.RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            ActionExecutionDelegate next = () =>
            {
                var ctx = new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), Mock.Of<Controller>());
                return Task.FromResult(ctx);
            };

            FakeUserFilter fakeUserFilter = new FakeUserFilter();

            await fakeUserFilter.OnActionExecutionAsync(actionExecutingContext, next);
            CancellationToken cancelTokenSource = new CancellationToken();
            var stringContent = new StringContent("test");

            //Act
            var response = await testClient.PostAsync("/PostCreation", stringContent, cancelTokenSource);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = response.As<Post>();
            returnedPost.PostId.Should().Be(null);
            returnedPost.Filling.Should().Be("test");
        }
    }
}
