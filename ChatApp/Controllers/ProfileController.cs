using ChatApp.Common;
using ChatApp.Models;
using Model.Dao;
using Model.Marsk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Prifle
        ProfileDao profileDao = new ProfileDao();
        public ActionResult Index()
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            ProfileUser profileUser = profileDao.getUserProfile(userLogin.Username.Trim());
            ProfileModel profileModel = new ProfileModel();
            profileModel.Active = profileUser.Active;
            profileModel.Birth = profileUser.Birth;
            profileModel.Connectionstring = profileUser.Connectionstring;
            profileModel.Email = profileUser.Email;
            profileModel.Id = profileUser.Id;
            profileModel.Nameshow = profileUser.Nameshow;
            profileModel.Pasword = profileUser.Pasword;

            profileModel.Pathimage = userLogin.Pathimage.Trim();

            profileModel.Role = profileUser.Role;
            profileModel.Sex = profileUser.Sex;
            profileModel.Username = profileUser.Username;
            return View(profileModel);
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            try
            {
                string fileName = file.FileName;
                string typeImage = "";
                bool flat = false;
                for (int i = 0; i < fileName.Length; i++)
                {
                    if (fileName[i] == '.')
                    {
                        flat = true;
                    }
                    if (flat)
                    {
                        typeImage += fileName[i];
                    }
                }
                file.SaveAs(Server.MapPath("~/ImagesProfileTemp/") + userLogin.Id.Trim() + typeImage);
                return "/ImagesProfileTemp/" + userLogin.Id.Trim() + typeImage;
            }
            catch (Exception e)
            {
                return "";
            }

        }
        public void SaveChange(string pathImage, string nameshow, string email, string birthdate, string password)
        {
            string filename = "";
            if (pathImage.Length != 0)
            {
                filename = pathImage.Substring(19, pathImage.Length - 19);
                string from = pathImage.Substring(1, pathImage.Length - 1);
                string to = "ImagesProfile/" + filename;

                from = from.Replace('/', '\\');
                to = to.Replace('/', '\\');

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                from = basePath + from;
                to = basePath + to;
                if (System.IO.File.Exists(to))
                {
                    System.IO.File.Delete(to);
                }
                System.IO.File.Move(from, to);
            }

            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            string md5 = UserDao.GetMD5(userLogin.Username.Trim() + password.Trim());
            profileDao.SaveChange(userLogin.Id.Trim(), "/ImagesProfile/" + filename, nameshow.Trim(), email.Trim(), birthdate.Trim(), password.Trim(), md5);

            UserDao usDao = new UserDao();

            var userSession = usDao.GetUserLoginInfo(userLogin.Username.Trim());
            userSession.Pathimage = userSession.Pathimage.Trim() + "?" + DateTime.Now.Ticks.ToString();
            Session.Add(CommonConstants.USER_SESSION, userSession);

            CommonClearCache.ClearCacheItems(Response);//Clear cache
        }
    }
}