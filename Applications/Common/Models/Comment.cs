namespace dotNet_TWITTER.Applications.Common.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string PostId { get; set; }
        public string UserName { get; set; }
        public string CommentFilling { get; set; }
        Post post { get; set; }
    }
}
