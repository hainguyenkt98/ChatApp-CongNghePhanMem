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
    public class BlockFriendController : BaseController
    {
        // GET: BlockFriend
        BlockFriendDao blockDao = new BlockFriendDao();
        public ActionResult Index()
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index");
            }

            BlockFriendModel blockModel = new BlockFriendModel();
            List<FriendInfo> lstfriF = blockDao.GetBlockListFriend(userLogin.Id);
            blockModel.ListFriendInfo = lstfriF;
            blockModel.UserLogin = userLogin;
            return View(blockModel);
        }
        [HttpPost]
        public JsonResult Finding(string keyword)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            List<FriendInfo> listSearchFriend = blockDao.Finding(keyword, userLogin.Id.Trim());
            return Json(listSearchFriend, JsonRequestBehavior.AllowGet);
        }
        public void UnBlock(string userID)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            blockDao.UnBlockFriend(userLogin.Id.Trim(), userID);
        }
    }
}