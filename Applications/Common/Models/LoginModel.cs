using System.ComponentModel.DataAnnotations;

namespace dotNet_TWITTER.Applications.Common.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
