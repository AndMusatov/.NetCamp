using dotNet_TWITTER.WEB_UI;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Twitter.IntegrationTests.Base
{
    public abstract class TestBase : IClassFixture<TestApplicationFactory<Startup, FakeStartup>>
    {
        protected WebApplicationFactory<FakeStartup> Factory { get; }

        public TestBase(TestApplicationFactory<Startup, FakeStartup> factory)
        {
            Factory = factory;
        }
    }
}
