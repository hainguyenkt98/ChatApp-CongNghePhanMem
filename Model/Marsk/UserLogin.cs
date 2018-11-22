using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Marsk
{
    public class UserLogin
    {
        public UserLogin()
        {
        }
        private string id = "";
        private string username = "";
        private string email = "";
        private DateTime birth;
        private string sex = "";
        private string nameshow = "";
        private bool active;
        private string role = "";
        private string connectionstring = "";
        private string pathimage = "";

        public string Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Birth { get => birth; set => birth = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Nameshow { get => nameshow; set => nameshow = value; }
        public bool Active { get => active; set => active = value; }
        public string Role { get => role; set => role = value; }
        public string Connectionstring { get => connectionstring; set => connectionstring = value; }
        public string Pathimage { get => pathimage; set => pathimage = value; }
    }
}
