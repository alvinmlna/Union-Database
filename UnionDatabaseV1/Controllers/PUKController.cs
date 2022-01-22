using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnionDatabaseV1.DAL;

namespace UnionDatabaseV1.Controllers
{
    public class PUKController : Controller
    {
        private ConnectionString db = new ConnectionString();

        // GET: PUK
        public ActionResult Index()
        {
            return View(db.PUKs.ToList());
        }

        // GET: PUK/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUK pUK = db.PUKs.Find(id);
            if (pUK == null)
            {
                return HttpNotFound();
            }
            return View(pUK);
        }

        // GET: PUK/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PUK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PUK1")] PUK pUK)
        {
            if (ModelState.IsValid)
            {
                db.PUKs.Add(pUK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pUK);
        }

        // GET: PUK/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUK pUK = db.PUKs.Find(id);
            if (pUK == null)
            {
                return HttpNotFound();
            }
            return View(pUK);
        }

        // POST: PUK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PUK1")] PUK pUK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pUK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pUK);
        }

        // GET: PUK/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUK pUK = db.PUKs.Find(id);
            if (pUK == null)
            {
                return HttpNotFound();
            }
            return View(pUK);
        }

        // POST: PUK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PUK pUK = db.PUKs.Find(id);
            db.PUKs.Remove(pUK);
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
