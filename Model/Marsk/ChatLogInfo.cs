using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Marsk
{
    public class ChatLogInfo
    {
        private string conversationid;
        private string name;
        private string lastmessage;
        private DateTime lastmodified;
        private bool readstatus;
        private bool onlinestatus;
        private string pathImage;
        private bool isgroup;
        public string Conversationid { get => conversationid; set => conversationid = value; }
        public string Name { get => name; set => name = value; }
        public string Lastmessage { get => lastmessage; set => lastmessage = value; }
        public DateTime Lastmodified { get => lastmodified; set => lastmodified = value; }
        public bool Readstatus { get => readstatus; set => readstatus = value; }
        public bool Onlinestatus { get => onlinestatus; set => onlinestatus = value; }
        public string PathImage { get => pathImage; set => pathImage = value; }
        public bool Isgroup { get => isgroup; set => isgroup = value; }
    }
}
