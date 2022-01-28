using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Data._enum;

namespace UnionDatabaseV1.Data.Services
{
    public interface IAppCoreService
    {
        //Check apakah user ini memiliki akses untuk mengelola puk
        bool IsHaveAccessToThisArea(string puk);
        bool? Login(string memberId, string password);

        bool IsUser(AccessEnum val);

        User GetCurrentUser();

        void Logout();
    }
}