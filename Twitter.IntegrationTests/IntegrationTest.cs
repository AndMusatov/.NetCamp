using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.WEB_UI;
using Microsoft.Extensions.DependencyInjection.Extensions;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Applications.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using dotNet_TWITTER.Domain.Events;
using Xunit;
using System.Threading;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Twitter.IntegrationTests
{
    class IntegrationTest
    {
        protected readonly HttpClient testClient;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(UserContext));
                        services.AddDbContext<UserContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                    builder.ConfigureServices(services =>
                    {
                        services.AddMvc(options =>
                        {
                            options.Filters.Add(new AllowAnonymousFilter());
                            options.Filters.Add(new FakeUserFilter());
                        })
                        .AddApplicationPart(typeof(Startup).Assembly);
                    });
                });
            testClient = appFactory.CreateClient();
        }

        /*protected async Task AuthenticateAsync()
        {
            testClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            RegisterModel registerModel = new RegisterModel
            {
                Email = "aaa@gmail.com",
                Password = "1111",
                ConfirmPassword = "1111",
                UserName = "testUser"
            };
            CancellationToken cancelTokenSource = new CancellationToken();
            var stringContent = new StringContent(registerModel.ToString());

            var response = await testClient.PostAsync("/Registration", stringContent, cancelTokenSource);
            var registrationResponse = response.Content.As<string>();
            return registrationResponse;
        }*/
    }
}
