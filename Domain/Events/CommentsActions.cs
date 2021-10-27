using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet_TWITTER.Applications.Common.Models;
using dotNet_TWITTER.Applications.Data;
using dotNet_TWITTER.Domain.DTO;

namespace dotNet_TWITTER.Domain.Events
{
    public class CommentsActions
    {
        public List<Post> Posts = new List<Post>();
        public CommentsActions()
        {
            Posts = IPostDataBase.GetPostsList();
        }

        public Comment GetComment(int postId, int commentId)
        {
            return Posts[postId].Comments[commentId];
        }

        public List<Comment> GetComments(int postId)
        {
            return Posts[postId].Comments;
        }

        public string AddComment(int postId, string commentStr)
        {
            if (LoginStatusDTO.loginStatus.Status)
            {
                if (LoginStatusDTO.loginStatus.LoginUser.UserComments == null)
                {
                    LoginStatusDTO.loginStatus.LoginUser.UserComments = new List<Comment>();
                }
                if (CommentsInputCheck(postId, commentStr))
                {
                    AddPostComment(postId, commentStr);
                    LoginStatusDTO.loginStatus.LoginUser.UserComments.Add(new Comment
                    {
                        PostId = postId,
                        UserName = LoginStatusDTO.loginStatus.LoginUser.UserName,
                        CommentId = Posts[postId].Comments.Count,
                        CommentFilling = commentStr
                    }
                    );
                    return "Input is Ok";
                }
                return "Input is wrong";
            }
            return "You aren`t logined";
        }

        public bool CommentsInputCheck(int postId, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return false;
            }
            if (Posts.Count <= postId)
            {
                return false;
            }
            return true;
        }

        public void AddPostComment(int postId, string comment)
        {
            if (LoginStatusDTO.loginStatus.LoginUser.UserPosts == null)
            {
                LoginStatusDTO.loginStatus.LoginUser.UserPosts = new List<Post>();
            }
            Posts = LoginStatusDTO.loginStatus.LoginUser.UserPosts;
            Posts[postId].Comments.Add(
                new Comment
                {
                    PostId = postId,
                    UserName = LoginStatusDTO.loginStatus.LoginUser.UserName,
                    CommentId = Posts[postId].Comments.Count,
                    CommentFilling = comment
                });
            IUserDataBase.SetUsersPosts(Posts, LoginStatusDTO.loginStatus.LoginUser.UserId);
        }
    }
}
