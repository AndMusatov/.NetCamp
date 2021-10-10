using NUnit.Framework;
using dotNet_TWITTER.Applications.Data;

namespace Posts.Test
{
    public class PostsShowTests
    {
        [SetUp]
        public void Setup()
        {
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