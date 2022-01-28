using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Data.Services;

namespace UnionDatabaseV1.Controllers
{
    public class StructureManagementController : Controller
    {
        private readonly IAppCoreService appCoreService;

        public StructureManagementController(IAppCoreService appCoreService)
        {
            this.appCoreService = appCoreService;
        }
        
        private Entities db = new Entities();

        // GET: StructureManagement
        public ActionResult Index(string puk)
        {
            //Area tidak boleh kosong
            if (string.IsNullOrEmpty(puk))
                return View("Error");

            var akses = appCoreService.IsHaveAccessToThisArea(puk);
            if (akses == false)
                return View("NoAccess");


            ViewBag.puk = puk;
            IEnumerable<Kepengurusan> kepengurusans;
            if (puk == "all")
            {
                kepengurusans = db.Kepengurusans.Include(k => k.PUK);
            }
            else
            {
                kepengurusans = db.Kepengurusans.Include(k => k.PUK)
                                .Where(x => x.PUK.PUK1 == puk);

                ViewBag.IsAdmin1 = true;
            }

            return View(kepengurusans.ToList());
        }

        // GET: StructureManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kepengurusan kepengurusan = db.Kepengurusans.Find(id);
            if (kepengurusan == null)
            {
                return HttpNotFound();
            }
            return View(kepengurusan);
        }

        // GET: StructureManagement/Create
        public ActionResult Create(string puk)
        {
            //Area tidak boleh kosong
            if (string.IsNullOrEmpty(puk))
                return View("Error");

            var akses = appCoreService.IsHaveAccessToThisArea(puk);
            if (akses == false)
                return View("NoAccess");

            if (puk == "all")
            {
                //jadi yang tampil hanya yang belum didaftarkan aja
                var allRegistered = db.Kepengurusans.Select(x => x.PUK_ID).ToList();
                ViewBag.PUK_ID = new SelectList(db.PUKs.Where(x => !allRegistered.Contains(x.Id)), "Id", "PUK1");
            }
            else
            {
                ViewBag.PUK_ID = new SelectList(db.PUKs.Where(x => x.PUK1 == puk), "Id", "PUK1");
            }
            return View();
        }

        // POST: StructureManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PUK_ID,Ketua,Sekertaris,Bendahara,Waka1,Wasek1,Waka2,Wasek2,Waka3,Wasek3,Waka4,Wasek4,Waka5,Wasek5,Waka6,Wasek6")] Kepengurusan kepengurusan)
        {
            if (ModelState.IsValid)
            {
                db.Kepengurusans.Add(kepengurusan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //jadi yang tampil hanya yang belum didaftarkan aja
            var allRegistered = db.Kepengurusans.Select(x => x.PUK_ID).ToList();
            ViewBag.PUK_ID = new SelectList(db.PUKs.Where(x => !allRegistered.Contains(x.Id)), "Id", "PUK1", kepengurusan.PUK_ID);

            return View(kepengurusan);
        }

        // GET: StructureManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kepengurusan kepengurusan = db.Kepengurusans.Find(id);
            if (kepengurusan == null)
            {
                return HttpNotFound();
            }
            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1", kepengurusan.PUK_ID);
            return View(kepengurusan);
        }

        // POST: StructureManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PUK_ID,Ketua,Sekertaris,Bendahara,Waka1,Wasek1,Waka2,Wasek2,Waka3,Wasek3,Waka4,Wasek4,Waka5,Wasek5,Waka6,Wasek6")] Kepengurusan kepengurusan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kepengurusan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1", kepengurusan.PUK_ID);
            return View(kepengurusan);
        }

        // GET: StructureManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kepengurusan kepengurusan = db.Kepengurusans.Find(id);
            if (kepengurusan == null)
            {
                return HttpNotFound();
            }
            return View(kepengurusan);
        }

        // POST: StructureManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kepengurusan kepengurusan = db.Kepengurusans.Find(id);
            db.Kepengurusans.Remove(kepengurusan);
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
    }
}
