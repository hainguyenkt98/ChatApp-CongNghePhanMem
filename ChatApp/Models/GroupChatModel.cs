using Model.Marsk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class GroupChatModel
    {
        private UserLogin userLogin;
        private List<GroupChat> listGroupChat;
        private List<FriendInfo> listFriend;


        public UserLogin UserLogin { get => userLogin; set => userLogin = value; }
        public List<GroupChat> ListGroupChat { get => listGroupChat; set => listGroupChat = value; }
        public List<FriendInfo> ListFriend { get => listFriend; set => listFriend = value; }
    }
}