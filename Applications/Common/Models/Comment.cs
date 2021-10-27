namespace dotNet_TWITTER.Applications.Common.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string CommentFilling { get; set; }
    }
}
