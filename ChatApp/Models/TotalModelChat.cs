using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Marsk;

namespace ChatApp.Models
{
    public class TotalModelChat
    {
        private List<FriendInfo> listFriendInfo;
        private List<ChatLogInfo> listChatLogInfo;
        private UserLogin userLogin;

        public List<FriendInfo> ListFriendInfo { get => listFriendInfo; set => listFriendInfo = value; }
        public List<ChatLogInfo> ListChatLogInfo { get => listChatLogInfo; set => listChatLogInfo = value; }
        public UserLogin UserLogin { get => userLogin; set => userLogin = value; }
    }
}