using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập tài khoản")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string PassWord { set; get; }
    }
}