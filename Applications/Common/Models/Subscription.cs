using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class Subscription
    {
        [Key]
        public string SubscriptionId { get; set; }
        public string AuthUser { get; set; }
        public string SubUser { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
    }
}
