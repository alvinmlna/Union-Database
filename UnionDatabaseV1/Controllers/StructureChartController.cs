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

        [HttpPost]
        public ActionResult Chart(int areaId)
        {
            ViewBag.areaId = areaId;
            ViewBag.Name = db.PUKs.FirstOrDefault(x => x.Id == areaId).PUK1;
            List<PUK> puks = db.Kepengurusans.Include("puk").Select(x => x.PUK).ToList();
            return View("Index", puks);
        }

        public JsonResult ShowChart(int areaId)
        {
            var kepengurusan = db.Kepengurusans.FirstOrDefault(x => x.PUK_ID == areaId);

            StructureChartJson result = new StructureChartJson()
            {
                name = kepengurusan.Ketua,
                title = "Ketua",
                children = new StructureChartJson[]
                {
                    new StructureChartJson
                    {
                        name = "Lainnya",
                        title = "",
                        children = new StructureChartJson[]
                        {
                            new StructureChartJson
                            {
                                name = kepengurusan.Waka1,
                                title = "Waka Bidang I Pendidikan",
                                children = new StructureChartJson[]
                                {
                                    new StructureChartJson
                                    {
                                        name = kepengurusan.Wasek1,
                                        title = "Wasek Bidang I Pendidikan"
                                    }
                                }
                            },
                            new StructureChartJson
                            {
                                name = kepengurusan.Waka2,
                                title = "Waka Bidang II Organisasi",
                                children = new StructureChartJson[]
                                {
                                    new StructureChartJson
                                    {
                                        name = kepengurusan.Wasek2,
                                        title = "Wasek Bidang II Organisasi"
                                    }
                                }
                            },
                            new StructureChartJson
                            {
                                name = kepengurusan.Waka3,
                                title = "Waka Bidang III Hukum dan Pembelaan",
                                children = new StructureChartJson[]
                                {
                                    new StructureChartJson
                                    {
                                        name = kepengurusan.Wasek3,
                                        title = "Wasek Bidang III Hukum dan Pembelaan"
                                    }
                                }
                            },
                            new StructureChartJson
                            {
                                name = kepengurusan.Waka4,
                                title = "Waka Bidang IV PKB dan K2",
                                children = new StructureChartJson[]
                                {
                                    new StructureChartJson
                                    {
                                        name = kepengurusan.Wasek4,
                                        title = "Wasek Bidang IV PKB dan K2"
                                    }
                                }
                            },
                            new StructureChartJson
                            {
                                name = kepengurusan.Waka5,
                                title = "Waka Bidang V Infokum dan Sosek",
                                children = new StructureChartJson[]
                                {
                                    new StructureChartJson
                                    {
                                        name = kepengurusan.Wasek5,
                                        title = "Wasek Bidang V Infokum dan Sosek"
                                    }
                                }
                            },
                            new StructureChartJson
                            {
                                name = kepengurusan.Waka6,
                                title = "Waka Bidang VI Pemberdayaan Perempuan",
                                children = new StructureChartJson[]
                                {
                                    new StructureChartJson
                                    {
                                        name = kepengurusan.Wasek6,
                                        title = "Wasek Bidang VI Pemberdayaan Perempuan"
                                    }
                                }
                            }
                        }
                    },
                    new StructureChartJson
                    {
                        name = kepengurusan.Bendahara,
                        title = "Bendahara"
                    },
                    new StructureChartJson
                    {
                        name = kepengurusan.Sekertaris,
                        title = "Sekertaris"
                    }
                }
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}