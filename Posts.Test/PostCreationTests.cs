using NUnit.Framework;
using dotNet_TWITTER.Applications.Data;

namespace Posts.Test
{
    public class Tests
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
            var result = service.CreateTest(filling);
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
            var result = service.CreateTest(filling);
            //assert
            Assert.IsFalse(result);
        }
    }
}