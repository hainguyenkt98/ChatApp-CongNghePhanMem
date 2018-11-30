using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Marsk
{
    public class GroupChat
    {
        private string conversationid;
        private string name;
        private DateTime lastmodified;
        private List<UserConversation> listUserConversation;
        public string Conversationid { get => conversationid; set => conversationid = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Lastmodified { get => lastmodified; set => lastmodified = value; }
        public List<UserConversation> ListUserConversation { get => listUserConversation; set => listUserConversation = value; }
    }
}
