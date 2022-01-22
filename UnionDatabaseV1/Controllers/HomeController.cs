using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Data.Services;

namespace UnionDatabaseV1.Controllers
{
    public class HomeController : Controller
    {
        private ConnectionString db = new ConnectionString();
        private IAppCoreService coreService;
        private IMemberService memberService;

        public HomeController(
            IAppCoreService coreService,
            IMemberService memberService
        )
        {
            this.coreService = coreService;
            this.memberService = memberService;
        }

        public ActionResult Index()
        {
            var currentUser = coreService.GetCurrentUser();

            IEnumerable<FileManager> image = db.FileManagers.Where(x => x.Category == 1);

            if (currentUser.MemberID != "0000")
            {
                var imageSpecificPUK = db.FileManagers.Where(x => x.Category == 2 && x.PUK_ID == currentUser.PUK);
                image = image.Concat(imageSpecificPUK);
            }


            return View(image.ToList());
        }

        [HttpPost]
        public JsonResult NewChart()
        {
            var getData = memberService.GetChart();

            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Employee", System.Type.GetType("System.String"));
            dt.Columns.Add("Credit", System.Type.GetType("System.Int32"));

            foreach (var item in getData)
            {
                DataRow dr = dt.NewRow();
                dr["Employee"] = item.PUK;
                dr["Credit"] = item.Count;
                dt.Rows.Add(dr);
            }

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Login(string Username, string Password)
        {
            var loginStatus = coreService.Login(Username, Password);
            if (loginStatus != null)
            {
                if (loginStatus == true)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                return Json("Password salah", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Anggota tidak ditemukan", JsonRequestBehavior.AllowGet);
            }
        }
    }
}