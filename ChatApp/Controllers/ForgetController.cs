using BotDetect.Web.Mvc;
using ChatApp.Models;
using Model.Dao;
using Model.LinQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class ForgetController : Controller
    {
        // GET: Forget
        public ActionResult Index()
        {
            return View();
        }
        [SimpleCaptchaValidation("CapchaCode", "ForgetCapcha", "Mã xác nhận không đúng!")]
        [HttpPost]
        public ActionResult SendEmail(ForgotPasswordModel model)
        {
            UserDao userDao = new UserDao();
            if (ModelState.IsValid)
            {
                if (userDao.CheckEmailForgotPassword(model.Email.Trim()))
                {
                    var us = userDao.GetUserFromEmail(model.Email);
                    string senderID = "hainguyenkt98@gmail.com";
                    string senderPassword = "77181352";
                    string md5Confirm = us.md5confirm.Trim();

                    string body = string.Format("Chào {0} <BR/> Tài khoản của bạn được yêu cầu lấy lại mật khẩu, vui lòng click vào " +
                        "link dưới đây để thay đổi mật khẩu mới : \"{1}\" ",
            us.nameshow.Trim(), Url.Action("ConfirmEmail", "Forget", new { md5Confirm = md5Confirm.Trim(), email = model.Email.Trim() }, Request.Url.Scheme));

                    MailMessage mail = new MailMessage();
                    mail.To.Add(model.Email.Trim());
                    mail.From = new MailAddress(senderID);
                    mail.Subject = "Lấy lại mật khẩu HKTCHAT";
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    ViewBag.Success = "Đã gửi mail để lấy lại mật khẩu, vui lòng kiểm tra email.";

                }
                else
                {
                    ModelState.AddModelError("", "Email không tồn tại trong hệ thống hoặc chưa được kích hoạt.");
                }

            }
            return View("Index");
        }
        public async Task<ActionResult> ConfirmEmail(string md5Confirm, string email)
        {
            UserDao userDao = new UserDao();
            if (ModelState.IsValid)
                Session.Add("emailforget", email);
            if(userDao.ConfirmEmail(md5Confirm.Trim(), email) != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
            
        }
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                UserDao userDao = new UserDao();
                tbUser us = userDao.GetUserFromEmail(Session["emailforget"].ToString().Trim());

                if(us.password.Trim() == model.OldPassword.Trim())
                {
                    userDao.ChangePassword(Session["emailforget"].ToString().Trim(), model.NewPassword);
                    Session["emailforget"] = null;
                    return RedirectToAction("Succsess", "Forget");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu cũ không chính xác.");
                }               
            }

            return View("ConfirmEmail");
        }
        public ActionResult Succsess()
        {
            return View();
        }
    }
}