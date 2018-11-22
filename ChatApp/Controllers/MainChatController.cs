using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Marsk;
using Model.Dao;
using ChatApp.Common;

namespace ChatApp.Controllers
{
    public class MainChatController : BaseController
    {
        // GET: MainChat
        ChatDao chatDao = new ChatDao();
        public ActionResult Index()
        {
            UserLogin userLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (userLogin == null)
            {
                RedirectToAction("Index");
            }
            TotalModelChat totalMDC = new TotalModelChat();

            List<FriendInfo> lstfriF = chatDao.GetOnlineList(userLogin.Id);
            List<ChatLogInfo> lstChatLog = chatDao.GetChatLogConversation(userLogin.Id);

            totalMDC.ListFriendInfo = lstfriF;
            totalMDC.UserLogin = userLogin;
            totalMDC.ListChatLogInfo = lstChatLog;
            return View(totalMDC);            
        }
        [HttpPost]
        public JsonResult GetListMessage(string conversationID)
        {
            UserLogin usLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            List<MessageItem> listMessage = chatDao.GetListMessage(conversationID, usLogin);

            return Json(listMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void UpdateConnectionId(string connectionid)
        {
            UserLogin usLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            chatDao.UpdateConnectionId(usLogin.Id, connectionid);
        }
        [HttpPost]
        public JsonResult GetInfoOneFriend(string userid)
        {
            FriendInfo finF = chatDao.GetInfoOneFriend(userid);
            return Json(finF, JsonRequestBehavior.AllowGet);
        }
        public void SetComversationUnread(string conversationid)
        {
            UserLogin usLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            chatDao.setConversationUnread(conversationid, usLogin.Id);
        }
        public void SetComversationReaded(string conversationid)
        {
            UserLogin usLogin = (UserLogin)Session[CommonConstants.USER_SESSION];
            chatDao.setConversationRead(conversationid, usLogin.Id);
        }
    }
}