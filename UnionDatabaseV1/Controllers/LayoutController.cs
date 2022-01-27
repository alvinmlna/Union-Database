using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Data.Services;

namespace UnionDatabaseV1.Controllers
{
    public class LayoutController : Controller
    {
        private ConnectionString db = new ConnectionString();
        private IAppCoreService appCoreService;

        public LayoutController(
            IAppCoreService appCoreService
        )
        {
            this.appCoreService = appCoreService;
        }

        // GET: Layout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManajemenAnggota()
        {
            var currentUser = appCoreService.GetCurrentUser();

            IEnumerable<string> pukList;

            if (currentUser.Akses == 1)
            {
                pukList = db.PUKs.Select(x => x.PUK1);
            } else
            {
                pukList = db.PUKs.Where(x => x.Id == currentUser.PUK).Select(x => x.PUK1);
            };

            ViewBag.akses = currentUser.Akses;
            ViewBag.puk = currentUser.PUK1?.PUK1;

            return PartialView("ManajemenAnggota", pukList.ToList());
        }


        public ActionResult Login()
        {
            var currentUser = appCoreService.GetCurrentUser();
            return PartialView("Login", currentUser);
        }

        public ActionResult NavBar()
        {
            var currentUser = appCoreService.GetCurrentUser();
            return PartialView("NavBar", currentUser);
        }

        public ActionResult Logout()
        {
            appCoreService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}