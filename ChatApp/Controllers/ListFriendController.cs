using ChatApp.Common;
using ChatApp.Models;
using Model.Dao;
using Model.Marsk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class ListFriendController : BaseController
    {
        // GET: ListFriend
        ListFriendDao listDao = new ListFriendDao();
        public ActionResult Index()
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            ListFriendModel listFriendModel = new ListFriendModel();
            List<FriendInfo> listFriend = listDao.GetListFriend(userLogin.Id.Trim());

            listFriendModel.ListFriendInfo = listFriend;
            listFriendModel.UserLogin = userLogin;
            return View(listFriendModel);
        }
        [HttpPost]
        public void UnFriend(string userID)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            listDao.UnFriend(userLogin.Id.Trim(), userID);
        }
        [HttpPost]
        public void BlockFriend(string userID)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            listDao.BlockFriend(userLogin.Id.Trim(), userID);
        }
        [HttpPost]
        public JsonResult Finding(string keyword)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            List<FriendInfo> listSearchFriend = listDao.Finding(keyword, userLogin.Id.Trim());
            return Json(listSearchFriend, JsonRequestBehavior.AllowGet);
        }
    }
}