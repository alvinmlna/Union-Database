using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Data._enum;
using UnionDatabaseV1.Data.Services;
using UnionDatabaseV1.Security;

namespace UnionDatabaseV1.Controllers
{
    [UserAuthorization(Roles = "1")]
    public class UsersController : Controller
    {
        private Entities db = new Entities();

        private readonly IMemberService memberService;
        private readonly IAppCoreService appCoreService;

        public UsersController(
            IMemberService memberService,
            IAppCoreService appCoreService
        )
        {
            this.memberService = memberService;
            this.appCoreService = appCoreService;
        }

        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            IEnumerable<User> users = db.Users;

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
                users = users.Where(s => s.MemberName.Contains(searchString) || s.MemberID.Contains(searchString));
            }

            users = users.OrderBy(s => s.MemberID);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }



        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.PUK = new SelectList(db.PUKs, "Id", "PUK1");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberID,Akses,PUK,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var member = memberService.FindByMemberId(user.MemberID);
                if (member != null)
                {
                    user.MemberName = member.Name;
                    if (user.Akses == (int)AccessEnum.Admin)
                    {
                        user.PUK = user.PUK;
                    }

                    if (user.Akses == (int)AccessEnum.Inti)
                    {
                        user.PUK = null;
                    }


                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else
                {
                    ModelState.AddModelError("MemberID", "Anggota tidak ditemukan, mohon ditambahkan terlebih dahulu.");
                    ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", user.MemberID);
                    ViewBag.PUK = new SelectList(db.PUKs, "Id", "PUK1", user.PUK);
                    return View(user);
                }
            }

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", user.MemberID);
            ViewBag.PUK = new SelectList(db.PUKs, "Id", "PUK1", user.PUK);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.PUK = new SelectList(db.PUKs, "Id", "PUK1", user.PUK);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberID,Akses,PUK,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var tobeUpdate = db.Users.FirstOrDefault(x => x.Id == user.Id);
                tobeUpdate.Akses = user.Akses;

                tobeUpdate.Password = user.Password;

                if (user.Akses == (int)AccessEnum.Admin)
                {
                    tobeUpdate.PUK = user.PUK;
                }

                if (user.Akses == (int)AccessEnum.Inti)
                {
                    tobeUpdate.PUK = null;
                }

                db.Entry(tobeUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Name", user.MemberID);
            ViewBag.PUK = new SelectList(db.PUKs, "Id", "PUK1", user.PUK);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
