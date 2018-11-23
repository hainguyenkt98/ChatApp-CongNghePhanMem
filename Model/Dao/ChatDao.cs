using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.LinQ;
using Model.Marsk;

namespace Model.Dao
{
    public class ChatDao
    {
        private UserLogin usLogin;

        public UserLogin UsLogin { get => usLogin; set => usLogin = value; }

        public ChatDao(UserLogin usLogin)
        {
            this.UsLogin = usLogin;
        }
        public ChatDao()
        {
            this.UsLogin = UsLogin;
        }
        public List<FriendInfo> GetOnlineList(string userID)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var onlineList = context.funcOnlineList(userID);
                var offlineList = context.funcOfflineList(userID);


                onlineList = onlineList.OrderByDescending(m => m.lasttimemodified);
                offlineList = offlineList.OrderByDescending(m => m.lasttimemodified);

                List<FriendInfo> list = new List<FriendInfo>();
                foreach (var item in onlineList)
                {
                    FriendInfo finF = new FriendInfo();
                    finF.Birth = (DateTime)item.birth;
                    finF.Connectionid = item.connectionid.Trim();
                    finF.Conversationid = item.conversationid.Trim();
                    finF.Email = item.email.Trim();
                    finF.Friendid = item.friend_id.Trim();
                    finF.Id = item.user_id.Trim();
                    finF.Lastmodified = (DateTime)item.lasttimemodified;
                    finF.Nameshow = item.nameshow.Trim();
                    finF.Onlinestatus = (bool)item.onlinestatus;
                    finF.Sex = item.sex.Trim();
                    finF.PathImage = item.pathimage.Trim();
                    finF.ReadStatus = (bool)item.readstatus;
                    list.Add(finF);
                }
                foreach (var item in offlineList)
                {
                    FriendInfo finF = new FriendInfo();
                    finF.Birth = (DateTime)item.birth;
                    finF.Connectionid = item.connectionid.Trim();
                    finF.Conversationid = item.conversationid.Trim();
                    finF.Email = item.email.Trim();
                    finF.Friendid = item.friend_id.Trim();
                    finF.Id = item.user_id.Trim();
                    finF.Lastmodified = (DateTime)item.lasttimemodified;
                    finF.Nameshow = item.nameshow.Trim();
                    finF.Onlinestatus = (bool)item.onlinestatus;
                    finF.Sex = item.sex.Trim();
                    finF.PathImage = item.pathimage.Trim();
                    finF.ReadStatus = (bool)item.readstatus;
                    list.Add(finF);
                }
                return list;
            }
        }
        public List<ChatLogInfo> GetChatLogConversation(string userID)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                List<ChatLogInfo> listChatLogInf = new List<ChatLogInfo>();
                var listSMSConversation = context.funcConversationSMS(userID);
                listSMSConversation = listSMSConversation.OrderByDescending(m => m.lasttimemodified);
                foreach (var item in listSMSConversation)
                {
                    ChatLogInfo chinF = new ChatLogInfo();
                    chinF.Conversationid = item.conversationid.Trim();

                    chinF.Lastmodified = (DateTime)item.lasttimemodified;
                    if (item.name.Trim() == "")
                    {
                        var conversationName = context.funcFindConversationName(userID, item.conversationid.Trim());
                        foreach (var item2 in conversationName)
                        {
                            chinF.Name = item2.nameshow.Trim();
                            chinF.PathImage = item2.pathimage.Trim();
                            break;
                        }
                    }
                    else
                    {
                        chinF.Name = item.name.Trim();
                    }

                    chinF.Readstatus = (bool)item.readstatus;
                    var lastmessage = context.funcLastSMSConversation(item.conversationid.Trim());
                    foreach (var ms in lastmessage)
                    {
                        if (ms.contents.Trim().Length > 20)
                        {
                            chinF.Lastmessage = ms.contents.Trim().Substring(0, 20) + "...";
                        }
                        else
                        {
                            chinF.Lastmessage = ms.contents.Trim();
                        }
                        break;
                    }
                    chinF.Isgroup = (bool)item.isGroup;
                    chinF.Onlinestatus = (bool)context.funcCheckOnlineCS(item.conversationid.Trim());

                    listChatLogInf.Add(chinF);
                }
                return listChatLogInf;
            }
        }

        public List<MessageItem> GetListMessage(string conversationID, UserLogin usLogin)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                List<MessageItem> messageListItem = new List<MessageItem>();
                var messageList = context.funcGetListMessage(conversationID);
                messageList = messageList.OrderBy(m => m.creationtime);
                foreach (var item in messageList)
                {
                    MessageItem msItem = new MessageItem();
                    msItem.Contents = item.contents.Trim();
                    msItem.Creationtime = (DateTime)item.creationtime;

                    if (item.userid.Trim() == usLogin.Id.Trim())
                    {
                        msItem.Direction = "outbound";
                    }
                    else
                    {
                        msItem.Direction = "inbound";
                    }
                    msItem.Messageid = item.msid.Trim();
                    msItem.Userid = item.userid.Trim();

                    messageListItem.Add(msItem);
                    msItem.PathImage = item.pathimage.Trim();
                }
                return messageListItem;
            }
        }
        public void UpdateConnectionId(string userid, string connectionid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var us = context.tbUsers.Where(m => m.id.Trim() == userid).FirstOrDefault();
                us.connectionid = connectionid;
                us.onlinestatus = true;

                context.SubmitChanges();
            }
        }
        public void DeleteConnectionId(string connectionid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var us = context.tbUsers.Where(m => m.connectionid.Trim() == connectionid).FirstOrDefault();
                if (us == null)
                    return;
                us.connectionid = "";
                us.onlinestatus = false;

                context.SubmitChanges();
            }
        }

        //public void setOnlineStatus(string connectionid)
        //{
        //    using (ChatAppLQDataContext context = new ChatAppLQDataContext())
        //    {
        //        var us = context.tbUsers.Where(m => m.connectionid.Trim() == connectionid).FirstOrDefault();
        //        if (us == null)
        //            return;
        //        us.onlinestatus = true;
        //        context.SubmitChanges();
        //    }
        //}

        public List<ChatLogInfo> GetAllConversation(string userid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                List<ChatLogInfo> list = new List<ChatLogInfo>();

                var listAllConversation = context.funcGetAllConversation(userid);
                foreach (var item in listAllConversation)
                {
                    ChatLogInfo chinF = new ChatLogInfo();
                    chinF.Conversationid = item.conversationid.Trim();
                    chinF.Readstatus = (bool)item.readstatus;
                    list.Add(chinF);
                }
                return list;
            }
        }
        public FriendInfo GetInfoOneFriend(string userid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                FriendInfo finF = new FriendInfo();
                var us = context.tbUsers.Where(m => m.id.Trim() == userid).FirstOrDefault();
                finF.Birth = (DateTime)us.birth;
                finF.Connectionid = us.connectionid.Trim();
                finF.Email = us.email.Trim();
                finF.Id = us.id.Trim();
                finF.Nameshow = us.nameshow.Trim();
                finF.Onlinestatus = (bool)us.onlinestatus;
                finF.PathImage = us.pathimage.Trim();
                finF.Sex = us.sex.Trim();
                return finF;
            }
        }
        public void InsertMessage(string contents, string userid, string conversationid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var messageid = context.funcMyNewIDMessage();
                tbMessage ms = new tbMessage();
                ms.id = messageid;
                ms.creationtime = DateTime.Now;
                ms.contents = contents;
                ms.conversationid = conversationid;
                ms.userid = userid;
                context.tbMessages.InsertOnSubmit(ms);
                context.SubmitChanges();
            }
        }
        public string getUseridFromConnectionid(string connectionid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var us = context.tbUsers.Where(m => m.connectionid.Trim() == connectionid).FirstOrDefault();
                if (us == null)
                    return "";
                return us.id.Trim();
            }
        }
        public void setConversationUnread(string conversationid, string userid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var listUser = context.tbConversation_Users.Where(m => m.conversationid.Trim() == conversationid && m.userid != userid);
                foreach (var item in listUser)
                {
                    var tbCU = context.tbConversation_Users.Where(m => m.conversationid.Trim() == conversationid && m.userid.Trim() == item.userid.Trim()).FirstOrDefault();
                    tbCU.readstatus = false;
                    context.SubmitChanges();
                }
            }
        }
        public void setConversationRead(string conversationid, string userid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var tbCU = context.tbConversation_Users.Where(m => m.conversationid.Trim() == conversationid && m.userid.Trim() == userid).FirstOrDefault();
                if (tbCU != null)
                {
                    tbCU.readstatus = true;
                    context.SubmitChanges();
                }
            }
        }
        public string getNameConversation(string conversationid, string userid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var conversation = context.tbConversations.Where(m => m.id.Trim() == conversationid).FirstOrDefault();
                if ((bool)conversation.isGroup == true)
                {
                    return conversation.name.Trim();
                }
                else
                {
                    var us = context.tbUsers.Where(m => m.id.Trim() == userid).FirstOrDefault();
                    return us.nameshow.Trim();
                }
            }
        }
        public string getIdUserFromSingleConversation(string conversationid, string userid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var conversation = context.tbConversations.Where(m => m.id.Trim() == conversationid).FirstOrDefault();
                if ((bool)conversation.isGroup == true)
                {
                    return "";
                }
                else
                {
                    var us = (from user in context.tbUsers
                              join conv in context.tbConversation_Users on user.id.Trim() equals conv.userid.Trim()
                              where user.id.Trim() != userid && conv.conversationid.Trim() == conversationid
                              select user).FirstOrDefault() ;
                    return us.id.Trim();
                }
            }
        }
    }
}

