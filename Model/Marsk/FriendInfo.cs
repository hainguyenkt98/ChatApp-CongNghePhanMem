using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Marsk
{
    public class FriendInfo
    {
        private string id;
        private string email;
        private DateTime birth;
        private string sex;
        private string nameshow;
        private string connectionid;
        private bool onlinestatus;
        private string pathImage;
        private string friendid;
        private string conversationid;
        private DateTime lastmodified;
        private bool readStatus;

        private bool isGroup;
        private string groupName = "";

        public string Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Birth { get => birth; set => birth = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Nameshow { get => nameshow; set => nameshow = value; }
        public string Connectionid { get => connectionid; set => connectionid = value; }
        public bool Onlinestatus { get => onlinestatus; set => onlinestatus = value; }
        public string PathImage { get => pathImage; set => pathImage = value; }
        public string Friendid { get => friendid; set => friendid = value; }
        public string Conversationid { get => conversationid; set => conversationid = value; }
        public DateTime Lastmodified { get => lastmodified; set => lastmodified = value; }
        public bool ReadStatus { get => readStatus; set => readStatus = value; }
        public bool IsGroup { get => isGroup; set => isGroup = value; }
        public string GroupName { get => groupName; set => groupName = value; }
    }
}
