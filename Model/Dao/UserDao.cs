using Model.LinQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Marsk;

namespace Model.Dao
{
    public class UserDao
    {
        public UserDao()
        {
            
        }
        public bool Login(string username, string password)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                tbUser us = context.tbUsers.Where(m => m.account.Trim() == username && m.password.Trim() == password).FirstOrDefault();
                if(us != null)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }
        public UserLogin GetUserLoginInfo(string username)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                tbUser us = context.tbUsers.Where(m => m.account.Trim() == username).FirstOrDefault();
                UserLogin usLogin = new UserLogin();
                usLogin.Username = us.account.Trim();
                usLogin.Id = us.id.Trim();
                usLogin.Nameshow = us.nameshow.Trim();
                usLogin.Role = us.role.Trim();
                usLogin.Sex = us.sex.Trim();
                usLogin.Email = us.email.Trim();
                usLogin.Connectionstring = us.connectionid.Trim();
                usLogin.Birth = (DateTime)us.birth;
                usLogin.Active = (bool)us.active;
                if(us.pathimage.Trim().Length == 0|| us.pathimage == null)
                {
                    usLogin.Pathimage = "~/Images/img-friend.png";
                }
                else
                {
                    usLogin.Pathimage = us.pathimage;
                }
                return usLogin;
            }
        }
    }
}
