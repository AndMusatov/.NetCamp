using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using dotNet_TWITTER.Models;
using System.Web;

namespace dotNet_TWITTER.Pages
{
    public class IndexModel : PageModel
    {
        public int a { get; set; } = 2;
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime PostDateTime { get; set; }
        public string PostFilling { get; set; }
        public bool IsCorrect { get; set; } = true;

        public void Create_Click(object sender, EventArgs e)
        {
            a++;
        }
    }
}