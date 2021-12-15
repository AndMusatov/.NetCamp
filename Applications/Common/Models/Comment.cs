using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string PostId { get; set; }
        public string UserName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string CommentFilling { get; set; }
        public Post post { get; set; }
    }
}
