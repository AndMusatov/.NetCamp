using NUnit.Framework;
using dotNet_TWITTER.Applications.Data;
using FluentAssertions;

namespace Posts.Test
{
    public class MainPostsTests
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
            var service = new IPostDataBase();
            var actions = new MainPostsActions();
            //act
            var result = actions.CanCreatePost(filling);
            //assert
            //Assert.AreEqual(expectedResult, result);
            result.Should().Be(expectedResult);
        }

        [Test]
        public void ShowingPosts_ShouldReturnTrue()
        {
            //arrange
            var service = new IPostDataBase();
            var actions = new MainPostsActions();
            //act
            var result = actions.PostInitCheck();
            //assert
            Assert.IsTrue(result);
        }
    }
}