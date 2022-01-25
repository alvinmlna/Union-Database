using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Security;

namespace UnionDatabaseV1.Controllers
{
    [UserAuthorization(Roles = "1")]
    public class FileManagersController : Controller
    {
        private ConnectionString db = new ConnectionString();

        // GET: FileManagers
        public ActionResult Index()
        {
            var fileManagers = db.FileManagers;
            return View(fileManagers.ToList());
        }

        // GET: FileManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileManager fileManager = db.FileManagers.Find(id);
            if (fileManager == null)
            {
                return HttpNotFound();
            }
            return View(fileManager);
        }

        // GET: FileManagers/Create
        public ActionResult Create()
        {
            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1");
            return View();
        }

        // POST: FileManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FileManager fileManager, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string _FileName = Path.GetRandomFileName() + " " + Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);

                    fileManager.Path = _FileName;
                }

                if (fileManager.Category == 1) //Semua area
                {
                    fileManager.PUK_ID = null;
                }


                db.FileManagers.Add(fileManager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1", fileManager.PUK_ID);
            return View(fileManager);
        }

        // GET: FileManagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileManager fileManager = db.FileManagers.Find(id);
            if (fileManager == null)
            {
                return HttpNotFound();
            }
            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1", fileManager.PUK_ID);
            return View(fileManager);
        }

        // POST: FileManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Path,Name,Category,PUK_ID")] FileManager fileManager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileManager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PUK_ID = new SelectList(db.PUKs, "Id", "PUK1", fileManager.PUK_ID);
            return View(fileManager);
        }

        // GET: FileManagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileManager fileManager = db.FileManagers.Find(id);
            if (fileManager == null)
            {
                return HttpNotFound();
            }
            return View(fileManager);
        }

        // POST: FileManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileManager fileManager = db.FileManagers.Find(id);

            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileManager.Path);
            if (System.IO.File.Exists(_path))
            {
                System.IO.File.Delete(_path);
            }

            db.FileManagers.Remove(fileManager);
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
