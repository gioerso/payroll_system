using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class usersController : Controller
    {
        private kursach_pm2Entities db = new kursach_pm2Entities();

        public ViewResult Index(string sortOrder, string searchString, string searchID)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";
            var users = from s in db.users select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.fio.Contains(searchString));

            }
            ViewBag.Message = "Hello world!";

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.fio);
                    break;
                case "id":
                    users = users.OrderBy(s => s.id);
                    break;
                case "id_desc":
                    users = users.OrderByDescending(s => s.id);
                    break;
                default:
                    users = users.OrderBy(s => s.fio);
                    break;
            }
            return View(users.ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.paynamentMethods, "userID", "userID");
            return View();
        }

        // POST: users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,login,password,fio,email,role, img")] users users, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        users.img = reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.paynamentMethods, "userID", "userID", users.id);
            return View(users);
        }

        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.paynamentMethods, "userID", "userID", users.id);
            return View(users);
        }

        // POST: users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,login,password,fio,email,role, img")] users users, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(users).State = EntityState.Modified;

                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            users.img = reader.ReadBytes(upload.ContentLength);
                        }
                        db.SaveChanges();
                    }

                    else
                    {
                        db.Entry(users).Property(m => m.img).IsModified = false;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                ViewBag.id = new SelectList(db.paynamentMethods, "userID", "userID", users.id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(users);
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            users users = db.users.Find(id);
            db.users.Remove(users);
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
