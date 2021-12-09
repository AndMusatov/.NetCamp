using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.WEB_UI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
using Xunit;

namespace Twitter.IntegrationTests
{
    public class AuthControllerTest
    {
        //WebApplicationFactory<Startup> _factory;

        public AuthControllerTest()
        {
            /*_factory = factory.WithWebHostBuilder(builder =>
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
            }); ;*/
        }

        [Fact]
        public async Task Registration_should_return_OK()
        {
            //Arrange
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
            HttpClient httpClient = factory.CreateClient();
            RegisterModel registerModel = new RegisterModel
            {
                Email = "bbbbcb@gmail.com",
                UserName = "User5c",
                Password = "!1111Cc",
                ConfirmPassword = "!1111Cc",
            };

            var json = JsonConvert.SerializeObject(registerModel.ToString());
            var data = new StringContent(content: json,
                 encoding: Encoding.UTF8,
                 mediaType: "application/json");

            //Act
            var response = await httpClient.PostAsync("https://localhost:44364/Registration?Email=bbbbcb@gmail.com&UserName=User5c&Password=!1111Cc&ConfirmPassword=!1111Cc", data);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            /*string getRegisterModel = await response.Content.ReadAsStringAsync();
            getRegisterModel.Should().Be(registerModel.ToString());*/
        }

        [Fact]
        public async Task Registration_should_return_BadRequest()
        {
            //Arrange
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
            HttpClient httpClient = factory.CreateClient();
            RegisterModel registerModel = new RegisterModel
            {
                Email = "bbbbcb@gmail.com",
                UserName = "User5c",
                Password = "!1111Cc",
                ConfirmPassword = "!1111Cc",
            };

            var payload = new Dictionary<string, string>
            {
              {"email", "bbbbcb@gmail.com"},
              {"userName", "User5c"},
              {"password", "!1111Cc"},
              {"confirmPassword", "!1111Cc"}
            };

            var json = JsonConvert.SerializeObject(payload);
            var data = new StringContent(content: json,
                 encoding: Encoding.UTF8,
                 mediaType: "application/json");

            //Act
            var response = await httpClient.PostAsync("/Registration", content: data);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            string getRegisterModel = await response.Content.ReadAsStringAsync();
            getRegisterModel.Should().Be(json);
        }
    }
}
