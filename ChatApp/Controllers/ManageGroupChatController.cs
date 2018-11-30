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
    public class ManageGroupChatController : BaseController
    {
        // GET: ManageGroupChat
        ListFriendDao listFriendDao = new ListFriendDao();
        GroupChatDao groupChatDao = new GroupChatDao();
        public ActionResult Index()
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            GroupChatModel groupChatModel = new GroupChatModel();
            groupChatModel.UserLogin = userLogin;
            groupChatModel.ListGroupChat = groupChatDao.getListGroupChat(userLogin.Id.Trim());
            groupChatModel.ListFriend = listFriendDao.GetListFriend(userLogin.Id.Trim());
            return View(groupChatModel);
        }
        public void CreateGroupChat(string[] listUserid, string groupName)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            Array.Resize(ref listUserid, listUserid.Length + 1);
            listUserid[listUserid.Length - 1] = userLogin.Id.Trim() ;
            groupChatDao.createConversation(listUserid, groupName);
        }
        public void LeaveConversation(string conversationid)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            groupChatDao.leaveConversation(userLogin.Id.Trim(), conversationid);
        }
        public JsonResult Finding(string keyword)
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index", "User");
            }
            List<GroupChat> listGroupChat = groupChatDao.Finding(keyword, userLogin.Id.Trim());
            return Json(listGroupChat, JsonRequestBehavior.AllowGet);
        }
    }
}