using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;

namespace dotNet_TWITTER.Domain.Events
{
    public class LikesActions
    {
        public List<Post> Posts = new List<Post>();
        public LikesActions()
        {
            Posts = IPostDataBase.GetPostsList();
        }

        public string AddLike(int postId)
        {
            if (LikeInputCheck(postId))
            {
                Posts[postId].Likes.Add("User");
                IPostDataBase.SetPostsList(Posts);
                return "Input is ok";
            }
            return "Wrong input Id";
        }

        public int PostLikesQuantity(int postId)
        {
            if (LikeInputCheck(postId))
            {
                return Posts[postId].Likes.Count;
            }
            return 0;
        }

        public bool LikeInputCheck(int postId)
        {
            if (postId >= Posts.Count)
            {
                return false;
            }
            return true;
        }
    }
}

