using Model.LinQ;
using Model.Marsk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class GroupChatDao
    {
        public GroupChatDao()
        { }

        public List<GroupChat> getListGroupChat(string userid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                List<GroupChat> listGroupChat = new List<GroupChat>();
                var listData = context.getListConversationGroupChat(userid);
                listData = listData.OrderByDescending(m => m.lasttimemodified);
                foreach (var item in listData)
                {
                    GroupChat grc = new GroupChat();
                    grc.Conversationid = item.conversationid.Trim();
                    grc.Name = item.name.Trim();
                    grc.Lastmodified = (DateTime)item.lasttimemodified;
                    grc.ListUserConversation = getListUserInConversation(item.conversationid.Trim());
                    listGroupChat.Add(grc);
                }
                return listGroupChat;
            }
        }
        public List<UserConversation> getListUserInConversation(string conversationid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                List<UserConversation> listUserConversation = new List<UserConversation>();
                var listData = context.funcGetAllUserConversation(conversationid);
                foreach (var item in listData)
                {
                    UserConversation usc = new UserConversation();
                    usc.Userid = item.userid.Trim();
                    usc.Email = item.email.Trim();
                    usc.Nameshow = item.nameshow.Trim();
                    listUserConversation.Add(usc);
                }
                return listUserConversation;
            }
        }

        public void createConversation(string[] listConversationId, string conversationName)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                string conversationid = context.funcMyNewIDConversation();
                context.NewConversation(conversationid, conversationName, true);
                for (int i = 0; i < listConversationId.Length; i++)
                {
                    context.AddUserToConversation(listConversationId[i].Trim(), conversationid);
                }
            }
        }
        public void leaveConversation(string userid, string conversationid)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                context.deleteUserFromConversation(userid, conversationid);
            }
        }
    }
}
