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
                if (us != null)
                {
                    return true;
                }
                else
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
                if (us.pathimage.Trim().Length == 0 || us.pathimage == null)
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
        public bool CheckExistUserName(string userName)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                if (context.tbUsers.Where(m => m.active == true).Count(m => m.account.Trim() == userName) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool CheckExistEmail(string userName)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                if (context.tbUsers.Where(m => m.active == true).Count(m => m.email.Trim() == userName) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void CreateUserName(string username, string password, string email, string name, string sex)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                tbUser us = new tbUser();
                us.id = context.funcMyNewIDUser();
                us.account = username;
                us.password = password;
                us.email = email;
                us.active = false;
                us.onlinestatus = false;
                us.role = "user";
                us.nameshow = name;
                us.md5confirm = GetMD5(username.Trim() + password.Trim());
                us.pathimage = "/Images/img-friend.png";
                us.sex = sex;
                us.birth = DateTime.Now;
                us.connectionid = "";
                context.tbUsers.InsertOnSubmit(us);
                context.SubmitChanges();
            }
        }
        public tbUser ConfirmEmail(string md5Confirm, string email)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                tbUser us = context.tbUsers.Where(m => m.md5confirm.Trim() == md5Confirm && m.email.Trim() == email.Trim()).FirstOrDefault();
                if (us != null)
                {
                    us.active = true;
                    context.SubmitChanges();
                }
                return us;
            }
        }
        public static String GetMD5(string txt)
        {
            String str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }
        public string GetMD5User(string username)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                return context.tbUsers.Where(m => m.account.Trim() == username).FirstOrDefault().md5confirm;
            }
        }
        public bool CheckEmailForgotPassword(string email)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var us = context.tbUsers.Where(m => m.email.Trim() == email && m.active == true).FirstOrDefault();
                if (us != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public tbUser GetUserFromEmail(string email)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                return context.tbUsers.Where(m => m.email.Trim() == email.Trim()).FirstOrDefault();
            }
        }
        public void ChangePassword(string email, string password)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                tbUser us = context.tbUsers.Where(m => m.email.Trim() == email.Trim()).FirstOrDefault();
                us.password = password.Trim();
                us.md5confirm = GetMD5(us.account.Trim() + us.password.Trim());
                context.SubmitChanges();
            }
        }
    }
}
