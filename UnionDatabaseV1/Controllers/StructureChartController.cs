using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnionDatabaseV1.Content;
using UnionDatabaseV1.DAL;

namespace UnionDatabaseV1.Controllers
{
    public class StructureChartController : Controller
    {
        private Entities db = new Entities();

        // GET: StructureChart
        public ActionResult Index()
        {
            List<PUK> puks = db.Kepengurusans.Include("puk").Select(x => x.PUK).ToList();

            ViewBag.areaId = puks.FirstOrDefault().Id;
            ViewBag.Name = puks.FirstOrDefault().PUK1;
            return View(puks);
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ShowChart(int areaId)
        {
            var kepengurusan = db.Kepengurusans.FirstOrDefault(x => x.PUK_ID == areaId);
            List<TempClass> list = new List<TempClass>();

            var ketua = new TempClass
            {
                id = "Ketua",
                title = "Ketua",
                name = kepengurusan.Ketua
            };
            list.Add(ketua);

            var sekertaris = new TempClass
            {
                id = "Sekertaris",
                title = "Sekertaris",
                name = kepengurusan.Sekertaris
            };

            list.Add(sekertaris);

            var bendahara = new TempClass
            {
                id = "Bendahara",
                title = "Bendahara",
                name = kepengurusan.Bendahara
            };
            list.Add(bendahara);

            var waka1 = new TempClass
            {
                id = "Waka1",
                title = "Waka Bidang I Pendidikan",
                name = kepengurusan.Waka1
            };

            var waka2 = new TempClass
            {
                id = "Waka2",
                title = "Waka Bidang II Organisasi",
                name = kepengurusan.Waka2
            };

            var waka3 = new TempClass
            {
                id = "Waka3",
                title = "Waka Bidang III Hukum dan Pembelaan",
                name = kepengurusan.Waka3
            };
            var waka4 = new TempClass
            {
                id = "Waka4",
                title = "Waka Bidang IV PKB dan K2",
                name = kepengurusan.Waka4
            };

            var waka5 = new TempClass
            {
                id = "Waka5",
                title = "Waka Bidang V Infokum dan Sosek",
                name = kepengurusan.Waka5
            };

            var waka6 = new TempClass
            {
                id = "Waka6",
                title = "Waka Bidang VI Pemberdayaan perempuan",
                name = kepengurusan.Waka6
            };

            var wasek1 = new TempClass
            {
                id = "Wasek1",
                title = "Wasek Bidang I Pendidikan",
                name = kepengurusan.Wasek1
            };
            var wasek2 = new TempClass
            {
                id = "Wasek2",
                title = "Wasek Bidang II Organisasi",
                name = kepengurusan.Wasek2
            };
            var wasek3 = new TempClass
            {
                id = "Wasek3",
                title = "Wasek Bidang III Hukum dan Pembelaan",
                name = kepengurusan.Wasek3
            };
            var wasek4 = new TempClass
            {
                id = "Wasek4",
                title = "Wasek Bidang IV PKB dan K2",
                name = kepengurusan.Wasek4
            };
            var wasek5 = new TempClass
            {
                id = "Wasek5",
                title = "Wasek Bidang V Infokum dan Sosek",
                name = kepengurusan.Wasek5
            };
            var wasek6 = new TempClass
            {
                id = "Wasek6",
                title = "Wasek Bidang VI Pemberdayaan Perempuan",
                name = kepengurusan.Wasek6
            };


            list.Add(waka1);
            list.Add(waka2);
            list.Add(waka3);
            list.Add(waka4);
            list.Add(waka5);
            list.Add(waka6);
            list.Add(wasek1);
            list.Add(wasek2);
            list.Add(wasek3);
            list.Add(wasek4);
            list.Add(wasek5);
            list.Add(wasek6);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Chart(int areaId)
        {
            ViewBag.areaId = areaId;
            ViewBag.Name = db.PUKs.FirstOrDefault(x => x.Id == areaId).PUK1;
            List<PUK> puks = db.Kepengurusans.Include("puk").Select(x => x.PUK).ToList();
            return View("Index", puks);
        }
    }
}


public class TempClass
{
    public string id { get; set; }
    public string title { get; set; }
    public string name { get; set; }
}