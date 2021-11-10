using NUnit.Framework;
using dotNet_TWITTER.Applications.Data;

namespace Posts.Test
{
    class PostLikesTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1)]
        [TestCase(0)]
        public void PostLikes_ShouldReturnTrue(int id)
        {
            //arrange
            /*var service = new IPostDataBase();
            var action = new LikesActions();
            //act
            var result = action.LikeInputCheck(id);
            //assert
            Assert.IsTrue(result);*/
        }

        [TestCase(5)]
        [TestCase(6)]
        public void PostLikes_ShouldReturnFalse(int id)
        {
            //arrange
            /*var service = new IPostDataBase();
            var action = new LikesActions();
            //act
            var result = action.LikeInputCheck(id);
            //assert
            Assert.IsFalse(result);*/
        }
    }
}
