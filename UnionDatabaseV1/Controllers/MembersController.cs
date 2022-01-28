using LinqToExcel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Data.Services;
using UnionDatabaseV1.Security;

namespace UnionDatabaseV1.Controllers
{
    [UserAuthorization(Roles = "1,2")]
    public class MembersController : Controller
    {
        private readonly IAppCoreService appCoreService;
        private readonly IMemberService memberService;

        private Entities db = new Entities();

        public MembersController(
            IAppCoreService appCoreService,
            IMemberService memberService)
        {
            this.appCoreService = appCoreService;
            this.memberService = memberService;
        }

        // GET: Members
        public ActionResult Index(string searchString, string currentFilter, string puk, int? page, bool? isSuccess)
        {

            if (isSuccess != null)
            {
                ViewBag.IsSuccess = true;
            }

            //Area tidak boleh kosong
            if (string.IsNullOrEmpty(puk))
                return View("Error");

            var akses = appCoreService.IsHaveAccessToThisArea(puk);
            if (akses == false)
                return View("NoAccess");

            ViewBag.puk = puk;
            IEnumerable<Member> members;
            if (puk == "all")
            {
                members = db.Members
                    .Include(m => m.PUK);
            }
            else
            {
                members = db.Members
                    .Include(m => m.PUK)
                    .Where(x => x.PUK.PUK1 == puk);
            }

            if (searchString != null)

            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(s => s.Name.Contains(searchString) || s.MemberID.Contains(searchString));
            }

            members = members.OrderBy(s => s.PUK.PUK1).ThenBy(x => x.MemberID);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(members.ToPagedList(pageNumber, pageSize));
        }

        // GET: Members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create(string puk)
        {
            var _PUK = db.PUKs.FirstOrDefault(x => x.PUK1 == puk);
            if (_PUK == null)
                return Json("PUK Not found", JsonRequestBehavior.AllowGet);


            var akses = appCoreService.IsHaveAccessToThisArea(puk);
            if (akses == false)
                return View("NoAccess");


            ViewBag.puk_id = _PUK.Id;
            ViewBag.puk = _PUK.PUK1;

            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,Name,PUK_ID,Gender")] Member member)
        {
            var _PUK = db.PUKs.FirstOrDefault(x => x.Id == member.PUK_ID);

            if (ModelState.IsValid)
            {
                Member exist = db.Members.Find(member.MemberID);
                if (exist != null)
                {
                    ModelState.AddModelError("", "ID Anggota sudah digunakan untuk: " + exist.Name + " (" + exist.PUK.PUK1 +")");
                    ViewBag.puk_id = _PUK.Id;
                    ViewBag.puk = _PUK.PUK1;

                    return View(member);
                }

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index", new { puk = _PUK.PUK1 });
            }

            ViewBag.puk_id = _PUK.Id;
            ViewBag.puk = _PUK.PUK1;
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1", member.PUK_ID);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,Name,PUK_ID,Gender")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                var puk = db.PUKs.FirstOrDefault(x => x.Id == member.PUK_ID);
                return RedirectToAction("Index", new { puk = puk.PUK1 });
            }
            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1", member.PUK_ID);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileResult DownloadExcel()
        {
            string path = "/Content/Template.xlsx";
            return File(path, "application/vnd.ms-excel", "UploadTemplate.xlsx");
        }

        public ActionResult Upload(string puk)
        {
            //Area tidak boleh kosong
            if (string.IsNullOrEmpty(puk))
                return View("Error");

            var akses = appCoreService.IsHaveAccessToThisArea(puk);
            if (akses == false)
                return View("NoAccess");

            ViewBag.puk = puk;

            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase FileUpload, string puk)
        {
            var _PUK = db.PUKs.FirstOrDefault(x => x.PUK1 == puk);
            if (_PUK == null)
                return Json("PUK Not found", JsonRequestBehavior.AllowGet);


            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Content/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    excelFile.AddMapping("MemberID", "Nomor Anggota");
                    excelFile.AddMapping("Name", "Nama");
                    excelFile.AddMapping("Gender", "Jenis Kelamin");
                    var artistAlbums = from a in excelFile.Worksheet<Member>(sheetName) select a ;

                    //delete data yg sudah ada di database
                    memberService.DeleteByPUK(puk);

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.Name != "" && a.MemberID != "" && a.Gender != "")
                            {
                                Member TU = new Member();
                                TU.MemberID = a.MemberID;
                                TU.Name = a.Name;
                                TU.Gender = a.Gender;
                                TU.PUK_ID = _PUK.Id;
                                db.Members.Add(TU);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.Name == "" || a.Name == null) data.Add("<li> name is required</li>");
                                if (a.MemberID == "" || a.MemberID == null) data.Add("<li> Address is required</li>");
                                if (a.Gender == "" || a.Gender == null) data.Add("<li>ContactNo is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }
                    //deleting excel file from folder
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return RedirectToAction("Index", new { puk = puk , isSuccess  = true });
                }
                else
                {
                    //alert message for invalid file format
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SearchMember(string Prefix)
        {
            var result = db.Members.Where(x => x.Name.Contains(Prefix))
                .Select(x => new { 
                    Name = x.Name,
                    Value = x.MemberID
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
