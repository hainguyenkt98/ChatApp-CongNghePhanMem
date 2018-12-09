using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Nhập mật khẩu cũ.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu mới.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Nhập lại mật khẩu mới không đúng!")]
        public string NewPasswordConfirm { get; set; }
    }
}