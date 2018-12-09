using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản.")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Độ dài tài khoản ít nhất 8 kí tự.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu !")]
        [StringLength(20,MinimumLength = 8, ErrorMessage = "Độ dài mật khẩu ít nhất 8 kí tự.")]
        public string PassWord { get; set; }

        [Compare("PassWord", ErrorMessage = "Nhập lại mật khẩu không đúng!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Độ dài tên ít nhất 1 kí tự.")]
        public string Name { get; set; }

        public bool Male { get; set; }
        public bool Female { get; set; }
    }
}