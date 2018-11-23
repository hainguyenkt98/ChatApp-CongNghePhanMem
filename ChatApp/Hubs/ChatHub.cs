using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatApp.Hubs
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        ChatDao chatDao = new ChatDao();
        public override System.Threading.Tasks.Task OnConnected()
        {
            string connectionid = Context.ConnectionId;
            //Thong bao connection
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            string userid = chatDao.getUseridFromConnectionid(Context.ConnectionId.Trim());
            if (userid == "")
            {
                base.OnDisconnected(stopCalled);
            }
            var listOnlineFriend = chatDao.GetOnlineList(userid);
            List<string> listConnection = new List<string>();

            foreach (var item in listOnlineFriend)
            {
                listConnection.Add(item.Connectionid.Trim());
            }
            Clients.Clients(listConnection).notificationUserOffline(userid);

            string connetionid = Context.ConnectionId;
            chatDao.DeleteConnectionId(connetionid);
            //End Thong bao disconnetion
            return base.OnDisconnected(stopCalled);
        }

        [HubMethodName("SetupAllConversation")]
        public void setupAllConversation(string userid)
        {
            var listAllConversation = chatDao.GetAllConversation(userid);
            foreach (var item in listAllConversation)
            {
                JoinConversation(item.Conversationid);
            }

            var listOnlineFriend = chatDao.GetOnlineList(userid);
            List<string> listConnection = new List<string>();

            foreach (var item in listOnlineFriend)
            {
                listConnection.Add(item.Connectionid.Trim());
            }

            Clients.Clients(listConnection).notificationUserOnline(userid);
            //End thong bao online

        }
        public Task JoinConversation(string conversationid)
        {
            return Groups.Add(Context.ConnectionId, conversationid);
        }
        [HubMethodName("SendMessage")]
        public void sendMessage(string content, string userid, string conversationid)
        {
            // Clients.Group(conversationid).sendMessage(content, userid, conversationid);
            Clients.OthersInGroup(conversationid).sendMessage(content, userid, conversationid);
            chatDao.InsertMessage(content, userid, conversationid);
            
            chatDao.setConversationUnread(conversationid, userid);
            //Luu database
        }
    }
}