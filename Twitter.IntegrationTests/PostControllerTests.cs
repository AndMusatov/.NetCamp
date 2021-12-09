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
    public class PostControllerTests 
    {

        public PostControllerTests()
        {
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            bool hasUser = false;
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
            }); ;
            UserContext userContext = factory.Services.CreateScope().ServiceProvider.GetService<UserContext>();

            List<Comment> comments = new List<Comment>() { new Comment { CommentId = "1"}, new Comment { CommentId = "2" }, new Comment { CommentId = "3" } };   
            await userContext.Comment.AddRangeAsync(comments);
            await userContext.SaveChangesAsync();
            HttpClient httpClient = factory.CreateClient();
            PostsParameters postsParameters = new PostsParameters { PageNumber = 1, PageSize = 5 };
            var stringPayload = JsonConvert.SerializeObject(postsParameters);
            var stringContent = new StringContent(stringPayload);
            CancellationToken cancelTokenSource = new CancellationToken();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync("/ShowPostComments");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK); // Status Code 200-299
        }
    }
}
