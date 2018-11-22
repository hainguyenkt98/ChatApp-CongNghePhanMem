using Model.LinQ;
using Model.Marsk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.Dao
{
    public class ProfileDao
    {
        public ProfileUser getUserProfile(string username)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                tbUser us = context.tbUsers.Where(m => m.account.Trim() == username).FirstOrDefault();
                ProfileUser profile = new ProfileUser();
                profile.Username = us.account.Trim();
                profile.Pasword = us.password.Trim();
                profile.Id = us.id.Trim();
                profile.Nameshow = us.nameshow.Trim();
                profile.Role = us.role.Trim();
                profile.Sex = us.sex.Trim();
                profile.Email = us.email.Trim();
                profile.Connectionstring = us.connectionid.Trim();
                profile.Birth = (DateTime)us.birth;
                profile.Active = (bool)us.active;
                profile.Pathimage = us.pathimage;
                return profile;
            }
        }
        public void SaveChange(string userid, string pathImage, string nameshow, string email, string birthdate, string password)
        {
            using (ChatAppLQDataContext context = new ChatAppLQDataContext())
            {
                var user = (from us in context.tbUsers
                            where us.id == userid
                            select us).FirstOrDefault();
                user.nameshow = nameshow;
                if(pathImage.Trim() != "/ImagesProfile/")
                {
                    user.pathimage = pathImage;
                }
                user.email = email;
                user.birth = Convert.ToDateTime(birthdate);
                user.password = password;
                context.SubmitChanges();
            }
        }
    }
}
