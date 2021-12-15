using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.WEB_UI;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Twitter.IntegrationTests.Base;
using Xunit;

namespace Twitter.IntegrationTests
{
    public class AuthControllerTest : TestBase
    {
        public AuthControllerTest(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/Registration?Email=bbbbcb@gmail.com&UserName=User5c&Password=1111&ConfirmPassword=1111", HttpStatusCode.OK)]
        [InlineData("/Registration?Email=bbbbcb@gmail.com&UserName=User5c&Password=1111", HttpStatusCode.BadRequest)]
        public async Task Post_Registration_Endpoints_should_return_StatusCode(string url, HttpStatusCode httpStatusCode)
        {
            // Arrange
            WebApplicationFactory<Startup> factory = GetFactory(false);
            HttpClient httpClient = factory.CreateClient();

            var registerModel = new RegisterModel()
            {
                Email = "test@gmail.com",
                UserName = "TestUser",
                Password = "1111",
                ConfirmPassword = "1111"
            };
            var content = new StringContent(registerModel.ToString(), Encoding.UTF8, "application/json");
            //string jsonString = System.Text.Json.JsonSerializer.Serialize<RegisterModel>(registerModel);
            //var stringContent = new StringContent(jsonString);

            // Act
            var response = await httpClient.PostAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(httpStatusCode);
        }

        [Theory]
        [InlineData("/AuthLogin?UserName=User5c&Password=1111", HttpStatusCode.OK)]
        [InlineData("/AuthLogin?UserName=User5c", HttpStatusCode.BadRequest)]
        public async Task Post_Login_Endpoints_should_return_StatusCode(string url, HttpStatusCode httpStatusCode)
        {
            // Arrange
            WebApplicationFactory<Startup> factory = GetFactory(false);
            HttpClient httpClient = factory.CreateClient();

            var loginModel = new LoginModel()
            {
                UserName = "User5c",
                Password = "1111"
            };
            var content = new StringContent(loginModel.ToString(), Encoding.UTF8, "application/json");
            //string jsonString = System.Text.Json.JsonSerializer.Serialize<RegisterModel>(registerModel);
            //var stringContent = new StringContent(jsonString);

            // Act
            var response = await httpClient.PostAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(httpStatusCode);
        }

        [Fact]
        public async Task Get_EndPointsReturnsSuccessForRegularUser()
        {
            // Arrange
            WebApplicationFactory<Startup> factory = GetFactory(false);

            HttpClient httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "Test", options => { });
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
            // Act
            var response = await httpClient.GetAsync("/LoginUserEMail");

            // Assert
            response.Should().Be(HttpStatusCode.OK);
        }

        WebApplicationFactory<Startup> GetFactory(bool hasUser)
        {
            return new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
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
            }); 
        }
    }
}
