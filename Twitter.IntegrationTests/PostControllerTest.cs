using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Controllers;
using dotNet_TWITTER.Domain.Events;
using dotNet_TWITTER.WEB_UI;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Twitter.IntegrationTests.Base;
using Xunit;

namespace Twitter.IntegrationTests
{
    public class PostControllerTests : IClassFixture<MediaGalleryFactory<FakeStartup>>
    {
        private readonly WebApplicationFactory<FakeStartup> _factory;

        public PostControllerTests(MediaGalleryFactory<FakeStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Get_SecurePageIsAvailableForAuthenticatedUser()
        {
            // Arrange
            var client = GetFactory().CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Act
            var response = await client.GetAsync("/ShowAuthUserPosts");
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            response.Should().Be(HttpStatusCode.OK);
            //Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        private WebApplicationFactory<FakeStartup> GetFactory(bool hasUser = false)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile(configPath,
                                       optional: true,
                                       reloadOnChange: true);
                });

                //builder.UseSolutionRelativeContentRoot("MediaGallery");

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc(options =>
                    {
                        if (hasUser)
                        {
                            options.Filters.Add(new AllowAnonymousFilter());
                            options.Filters.Add(new FakeUserFilter());
                        }
                    })
                    .AddApplicationPart(typeof(Startup).Assembly);
                });
            });
        }
    }
}