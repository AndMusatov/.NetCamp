using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AuthUser { get; set; }
        public string SubUser { get; set; }
    }
}
