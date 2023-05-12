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
    public class accuralsController : Controller
    {
        private kursach_pm2Entities db = new kursach_pm2Entities();

        // GET: accurals
        public ActionResult Index()
        {
            var accurals = db.accurals.Include(a => a.users);
            return View(accurals.ToList());
        }

        // GET: accurals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accurals accurals = db.accurals.Find(id);
            if (accurals == null)
            {
                return HttpNotFound();
            }
            return View(accurals);
        }

        // GET: accurals/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.users, "id", "login");
            return View();
        }

        // POST: accurals/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userID,sum,date")] accurals accurals)
        {
            if (ModelState.IsValid)
            {
                db.accurals.Add(accurals);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.users, "id", "login", accurals.userID);
            return View(accurals);
        }

        // GET: accurals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accurals accurals = db.accurals.Find(id);
            if (accurals == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", accurals.userID);
            return View(accurals);
        }

        // POST: accurals/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userID,sum,date")] accurals accurals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accurals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", accurals.userID);
            return View(accurals);
        }

        // GET: accurals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accurals accurals = db.accurals.Find(id);
            if (accurals == null)
            {
                return HttpNotFound();
            }
            return View(accurals);
        }

        // POST: accurals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            accurals accurals = db.accurals.Find(id);
            db.accurals.Remove(accurals);
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
