using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Marsk
{
    public class MessageItem
    {
        private string messageid;
        private DateTime creationtime;
        private string contents;
        private string conversationid;
        private string userid;
        private string direction;
        private string pathImage;

        public string Messageid { get => messageid; set => messageid = value; }
        public DateTime Creationtime { get => creationtime; set => creationtime = value; }
        public string Contents { get => contents; set => contents = value; }
        public string Conversationid { get => conversationid; set => conversationid = value; }
        public string Userid { get => userid; set => userid = value; }
        public string Direction { get => direction; set => direction = value; }
        public string PathImage { get => pathImage; set => pathImage = value; }
    }
}
