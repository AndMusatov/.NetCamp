using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.Events;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Posts.Test
{
    public class PostsTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("ThirdPost", true)]
        public void CreationPost_Test(string filling, bool expectedResult)
        {
            //arrange
            /*_accountBalanceService = new AccountBalanceService(new AverageJoeBankService());
            //act
            var result = actions.CanCreatePost(filling);
            //assert
            //Assert.AreEqual(expectedResult, result);
            result.Should().Be(expectedResult);*/
        }
    }
}