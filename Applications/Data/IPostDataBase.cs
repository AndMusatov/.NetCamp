using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;

namespace dotNet_TWITTER.Applications.Data
{
    public class IPostDataBase
    {
        public static List<Post> PostsDB { get; set; }
        public IPostDataBase()
        {
            PostsDB = new List<Post>
            {
                new Post
                {
                    Id = 0,
                    Filling = "FirstPost",
                    Date = DateTime.Now,
                },
                new Post
                {
                    Id = 1,
                    Filling = "SecondPost",
                    Date = DateTime.Now,
                }
            };
        }

        public static Post SendPost(int i)
        {
            if (PostsDB.Count >= i)
            {
                return PostsDB[i];
            }
            return null;
        }

        public bool PostInitCheck()
        {
            if (PostsDB.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool AddPostInitCheck(string filling)
        {
            if (string.IsNullOrEmpty(filling))
            {
                return false;
            }
            return true;
        }

        public static string AddPost(string filling)
        {
            if (AddPostInitCheck(filling))
            {
                PostsDB.Add(
                new Post
                {
                    Id = PostsDB.Count,
                    Date = DateTime.Now,
                    Filling = filling
                }
                );
                return "Input is Ok";
            }
            return "Input is wrong";
        }

        public static Comment SendComment(int postId, int commentId)
        {
            return PostsDB[postId].Comments[commentId];
        }

        public static string AddComment(int postId, string comment)
        {
            if (CommentsInputCheck(postId, comment))
            {
                PostsDB[postId].Comments.Add(
                new Comment
                {
                    PostId = postId,
                    CommentFilling = comment
                }
                );
                return "Input is Ok";
            }
            return "Input is wrong";
        }

        public static bool CommentsInputCheck(int postId, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return false;
            }
            if (PostsDB.Count <= postId)
            {
                return false;
            }
            return true;
        }

        public static string AddLike(int postId)
        {
            if (LikeInputCheck(postId))
            {
                PostsDB[postId].Likes.Add("User");
                return "Input is ok";
            }
            return "Wrong input Id";
        }

        public static int PostLikesQuantity(int postId)
        {
            if (LikeInputCheck(postId))
            {
                return PostsDB[postId].Likes.Count;
            }
            return 0;
        }

        public static bool LikeInputCheck(int postId)
        {
            if (postId >= PostsDB.Count)
            {
                return false;
            }
            return true;
        }
    }
}
