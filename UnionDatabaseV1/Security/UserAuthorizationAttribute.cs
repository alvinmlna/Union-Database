using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UnionDatabaseV1.Data.Services;

namespace UnionDatabaseV1.Security
{
    public class UserAuthorizationAttribute : AuthorizeAttribute
    {
        private readonly IAppCoreService coreService;

        public UserAuthorizationAttribute()
        {
            coreService = DependencyResolver.Current.GetService<IAppCoreService>();
        }

        public UserAuthorizationAttribute(string Roles)
        {
            this.Roles = Roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = coreService.GetCurrentUser();

            var routeData = httpContext.Request.RequestContext.RouteData;

            bool authorize = false;
            List<int> allowedRole = Roles.Split(',')
                .Select(x => int.Parse(x))
                .ToList();

            foreach (var item in allowedRole)
            {
                if (currentUser.Akses == item)
                    authorize = true;
            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult { ViewName = "NoAccess" };
        }
    }
}