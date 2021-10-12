using NUnit.Framework;
using dotNet_TWITTER.Applications.Data;

namespace Posts.Test
{
    public class MainPostsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("ThirdPost")]
        public void CreationPost_ShouldReturnTrue(string filling)
        {
            //arrange
            var service = new IPostDataBase();
            //act
            var result = IPostDataBase.AddPostInitCheck(filling);
            //assert
            Assert.IsTrue(result);
        }

        [TestCase("")]
        [TestCase(null)]
        public void CreationPost_ShouldReturnFalse(string filling)
        {
            //arrange
            var service = new IPostDataBase();
            //act
            var result = IPostDataBase.AddPostInitCheck(filling);
            //assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShowingPosts_ShouldReturnTrue()
        {
            //arrange
            var service = new IPostDataBase();
            //act
            var result = service.PostInitCheck();
            //assert
            Assert.IsTrue(result);
        }
    }
}