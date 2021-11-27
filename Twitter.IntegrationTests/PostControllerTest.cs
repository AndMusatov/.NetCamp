using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.IntegrationTests.Base;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using dotNet_TWITTER.WEB_UI;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using Microsoft.Extensions.Hosting;

namespace Twitter.IntegrationTests
{
    public class PostControllerTests : IClassFixture<MediaGalleryFactory<FakeStartup>>
    {
        private readonly WebApplicationFactory<FakeStartup> _factory;

        public PostControllerTests(MediaGalleryFactory<FakeStartup> factory)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("MediaGallery");

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
            });
        }

        [NUnit.Framework.Theory]
        public async void Get_SecurePageIsAvailableForAuthenticatedUser()
        {
            // Arrange
            var client = _factory.CreateClient(
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
    }
}
