﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnionDatabaseV1.DAL;

namespace UnionDatabaseV1.Data.Services
{
    public interface IAppCoreService
    {
        //Check apakah user ini memiliki akses untuk mengelola puk
        bool IsHaveAccessToThisArea(string puk);
        void Login(string memberId);

        User GetCurrentUser();

        void Logout();
    }
}