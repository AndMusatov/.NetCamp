using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.WEB_UI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twitter.IntegrationTests.Base;
using Xunit;

namespace Twitter.IntegrationTests
{
    public class CommentControllerTests 
    {
        [Fact]
        public async Task Get_ShowPostComments_should_return_HttpStatusCodeOK()
        {
            // Arrange
            WebApplicationFactory<Startup> factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDesriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<UserContext>));

                    services.Remove(dbContextDesriptor);

                    services.AddDbContext<UserContext>(options =>
                    {
                        options.UseInMemoryDatabase("twitter_test_db");
                    });
                });
            }); ;

            UserContext userContext = factory.Services.CreateScope().ServiceProvider.GetService<UserContext>();
            List<Comment> comments = new List<Comment>() 
                { new Comment { CommentId = "1", PostId = "1" }, new Comment { CommentId = "2", PostId = "1" }, new Comment { CommentId = "3", PostId = "1" } };
            await userContext.Comment.AddRangeAsync(comments);
            Post post = new Post { PostId = "1"};
            await userContext.Post.AddRangeAsync(post);
            await userContext.SaveChangesAsync();
            HttpClient httpClient = factory.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync("/ShowPostComments?postId=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
