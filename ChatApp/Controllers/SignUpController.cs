using BotDetect.Web.Mvc;
using ChatApp.Common;
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
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [SimpleCaptchaValidation("CapchaCode", "SignupCapCha", "Mã xác nhận không đúng!")]
        public ActionResult Sign(SignUpModel model)
        {
            UserDao usDao = new UserDao();
            if (ModelState.IsValid)
            {
                if (usDao.CheckExistUserName(model.UserName.Trim()))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                }
                else
                {
                    if (usDao.CheckExistEmail(model.Email.Trim()))
                    {
                        ModelState.AddModelError("", "Email đã được sử dụng.");
                    }
                    else
                    {
                        string sex = "";
                        //Gửi mail xác nhận 
                        if (model.Female == true)
                        {
                            sex = "nữ";
                        }
                        else
                        {
                            sex = "nam";
                        }
                        usDao.CreateUserName(model.UserName.Trim(), model.PassWord.Trim(), model.Email.Trim(), model.Name.Trim(), sex);

                        string senderID = "hktchat@gmail.com";
                        string senderPassword = "Hai77181352";
                        string md5Confirm = usDao.GetMD5User(model.UserName.Trim());

                        string body = string.Format("Chào {0} <BR/> Cảm ơn bạn đã đăng ký tài khoản của chúng tôi, vui lòng click " +
                       " <a href = '{1}' > <b>Tại đây</b> </a> để kích hoạt tài khoản.",
           model.Name, Url.Action("ConfirmEmail", "SignUp", new { md5Confirm = md5Confirm.Trim(), email = model.Email.Trim() }, Request.Url.Scheme));

                        MailMessage mail = new MailMessage();
                        mail.To.Add(model.Email.Trim());
                        mail.From = new MailAddress(senderID);
                        mail.Subject = "Kích hoạt tài khoản HKTCHAT";
                        mail.Body = body;
                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                        smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(mail);

                        return RedirectToAction("Succsess", "SignUp");
                    }
                }
            }
            return View("Index");
        }
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string md5Confirm, string email)
        {
            UserDao usDao = new UserDao();
            tbUser us = usDao.ConfirmEmail(md5Confirm.Trim(), email);
            if (us != null)
            {

                var userSession = usDao.GetUserLoginInfo(us.account.Trim());
                Session.Add(CommonConstants.USER_SESSION, userSession);
                Session.Add(CommonConstants.USER_SESSION_ID, userSession.Id.Trim());
                return RedirectToAction("Index", "MainChat");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
        public ActionResult Succsess()
        {
            return View();
        }
    }
}