using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Security;

namespace UnionDatabaseV1.Controllers
{
    public class OrganizationalDataController : Controller
    {
        Entities db = new Entities();

        // GET: OrganizationalData

        [UserAuthorization(Roles = "1")]
        public ActionResult Index()
        {
            var allData = db.FileManagers.Where(x => x.Category == 3)
                .OrderBy(x => x.Name)
                .ToList();

            return View(allData);
        }

        public ActionResult Show()
        {
            var allData = db.FileManagers.Where(x => x.Category == 3)
                .OrderBy(x => x.Name)
                .ToList();

            return View(allData);
        }

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
            return View(fileManager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, HttpPostedFileBase file)
        {
            FileManager fileManager = db.FileManagers.Find(Id);

            if (file != null)
            {
                if (string.IsNullOrEmpty(fileManager.Path) == false)
                {
                    string _pathToDelete = Path.Combine(Server.MapPath("~/UploadedFiles"), fileManager.Path);
                    if (System.IO.File.Exists(_pathToDelete))
                    {
                        System.IO.File.Delete(_pathToDelete);
                    }
                }

                string _FileName = Path.GetRandomFileName() + " " + Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                file.SaveAs(_path);

                fileManager.Path = _FileName;
            }

            db.Entry(fileManager).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FileManager fileManager = db.FileManagers.Find(id);
            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileManager.Path);

            byte[] fileBytes = System.IO.File.ReadAllBytes(_path);
            string fileName = fileManager.Path;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}