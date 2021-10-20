using NUnit.Framework;
using dotNet_TWITTER.Applications.Data;

namespace Posts.Test
{
    class PostCommentsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(0, "abc")]
        [TestCase(1, "dcv")]
        public void PostComments_ShouldReturnTrue(int id, string filling)
        {
            //arrange
            var service = new IPostDataBase();
            var action = new CommentsActions();
            //act
            var result = action.CommentsInputCheck(id, filling);
            //assert
            Assert.IsTrue(result);
        }

        [TestCase(1, null)]
        [TestCase(2, null)]
        [TestCase(1, "")]
        [TestCase(50, "abc")]
        public void PostComments_ShouldReturnFalse(int id, string filling)
        {
            //arrange
            var service = new IPostDataBase();
            var action = new CommentsActions();
            //act
            var result = action.CommentsInputCheck(id, filling);
            //assert
            Assert.IsFalse(result);
        }
    }
}
