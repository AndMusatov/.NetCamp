using System;
using Microsoft.EntityFrameworkCore;

namespace dotNet_TWITTER.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Filling { get; set; }
        public DateTime Date { get; set; }
    }


}
