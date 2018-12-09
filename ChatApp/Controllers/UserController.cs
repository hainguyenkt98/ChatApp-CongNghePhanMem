using ChatApp.Common;
using ChatApp.Models;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                UserDao usDao = new UserDao();
                var result = usDao.Login(loginModel.UserName, loginModel.PassWord);

                if (result)
                {
                    var userSession = usDao.GetUserLoginInfo(loginModel.UserName.Trim());
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    Session.Add(CommonConstants.USER_SESSION_ID, userSession.Id.Trim());
                    return RedirectToAction("Index", "MainChat");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng !");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return View("Index");
        }
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Forgetpassword()
        {
            return View();
        }
    }
}