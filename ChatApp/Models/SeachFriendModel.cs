using Model.Marsk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class SeachFriendModel
    {
        private UserLogin userLogin;
        private List<FriendInfo> listFriendInfo;

        public UserLogin UserLogin { get => userLogin; set => userLogin = value; }
        public List<FriendInfo> ListFriendInfo { get => listFriendInfo; set => listFriendInfo = value; }
    }
}