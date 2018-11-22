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
    public class SearchFriendController : BaseController
    {
        // GET: FindFriend
        SearchDao searchDao = new SearchDao();
        public ActionResult Index()
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            SeachFriendModel searchFriendModel = new SeachFriendModel();
            List<FriendInfo> listFriend = searchDao.GetListSearchFriend(userLogin.Id.Trim());

            searchFriendModel.ListFriendInfo = listFriend;
            searchFriendModel.UserLogin = userLogin;
            return View(searchFriendModel);
        }
        [HttpPost]
        public void AddFriend(string userid)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            searchDao.AddFriend(userLogin.Id.Trim(), userid);
            //RedirectToAction("Index", "SearchFriend");
        }
        [HttpPost]
        public JsonResult Finding(string keyword)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            List<FriendInfo> listSearchFriend = searchDao.Finding(keyword, userLogin.Id.Trim());
            return Json(listSearchFriend, JsonRequestBehavior.AllowGet);
        }
    }
}