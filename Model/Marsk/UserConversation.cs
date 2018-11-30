using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Marsk
{
    public class UserConversation
    {
        private string userid;
        private string email;
        private string nameshow;

        public string Userid { get => userid; set => userid = value; }
        public string Email { get => email; set => email = value; }
        public string Nameshow { get => nameshow; set => nameshow = value; }
    }
}
