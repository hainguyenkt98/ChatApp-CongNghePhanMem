﻿using Model.LinQ;
using Model.Marsk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ListFriendDao
    {
        public ListFriendDao()
        { }
        public List<FriendInfo> GetListFriend(string userID)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                List<FriendInfo> listFriendInfo = new List<FriendInfo>();
                var listSearchFriend = context.funcGetListFriend(userID);
                foreach (var item in listSearchFriend)
                {
                    FriendInfo finF = new FriendInfo();
                    finF.Birth = (DateTime)item.birth;
                    finF.Connectionid = item.connectionid.Trim();
                    finF.Email = item.email.Trim();
                    finF.Id = item.id.Trim();
                    finF.Nameshow = item.nameshow.Trim();
                    finF.Onlinestatus = (bool)item.onlinestatus;
                    finF.PathImage = item.pathimage.Trim();
                    string temp = item.sex.Trim();
                    temp = char.ToUpper(temp[0]) + temp.Substring(1);
                    finF.Sex = temp;
                    listFriendInfo.Add(finF);
                }
                return listFriendInfo;
            }
        }
        public void UnFriend(string useridchudong,string useridbidong)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                context.UnFriend(useridchudong, useridbidong);
            }
        }
        public void BlockFriend(string useridchudong, string useridbidong)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                context.BlockFriend(useridchudong, useridbidong);
            }
        }

        public List<FriendInfo> Finding(string keyword, string userID)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                List<FriendInfo> listFriendInfo = new List<FriendInfo>();
                var listSearchFriend = context.funcGetListFriend(userID);
                listSearchFriend = listSearchFriend.Where(m => m.nameshow.Contains(keyword) || m.email.Contains(keyword));
                foreach (var item in listSearchFriend)
                {
                    FriendInfo finF = new FriendInfo();
                    finF.Birth = (DateTime)item.birth;
                    finF.Connectionid = item.connectionid.Trim();
                    finF.Email = item.email.Trim();
                    finF.Id = item.id.Trim();
                    finF.Nameshow = item.nameshow.Trim();
                    finF.Onlinestatus = (bool)item.onlinestatus;
                    finF.PathImage = item.pathimage.Trim();
                    string temp = item.sex.Trim();
                    temp = char.ToUpper(temp[0]) + temp.Substring(1);
                    finF.Sex = temp;
                    listFriendInfo.Add(finF);
                }
                return listFriendInfo;
            }
        }
    }

}
