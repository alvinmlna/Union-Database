using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnionDatabaseV1.DAL;

namespace UnionDatabaseV1.Data.Services
{
    public class AppCoreService : IAppCoreService
    {
        private readonly ConnectionString db;

        public AppCoreService(ConnectionString db)
        {
            this.db = db;
        }
        
        public User GetCurrentUser()
        {
            if (HttpContext.Current.Session["CurrentUser"] != null)
            {
                var memberId = HttpContext.Current.Session["CurrentUser"] as string;
                var registeredUser = db.Users.FirstOrDefault(x => x.MemberID == memberId);
                if (registeredUser != null)
                {
                    return registeredUser;
                } else
                {
                    var registeredMember = db.Members.FirstOrDefault(x => x.MemberID == memberId);
                    User anonymous = new User
                    {
                        Akses = 0,
                        MemberID = registeredMember.MemberID,
                        MemberName = registeredMember.Name,
                    };
                    return anonymous;
                }

            } else
            {
                User anonymous = new User
                {
                    Akses = 0,
                    MemberID = "0000",
                    MemberName = "0000",
                };
                return anonymous;
            }
        }

        public void Login(string memberId)
        {
            HttpContext.Current.Session["CurrentUser"] = memberId;
        }

        public bool IsHaveAccessToThisArea(string puk)
        {
            var currentUser = GetCurrentUser();
            var _puk = db.PUKs.FirstOrDefault(x => x.PUK1 == puk);
            
            if (currentUser.Akses == 1)
            {
                return true;
            }

            if (currentUser.Akses == 2 && currentUser.PUK == _puk.Id)
            {
                return true;
            }

            return false;
        }

        public void Logout()
        {
            HttpContext.Current.Session["CurrentUser"] = null;
        }
    }
}