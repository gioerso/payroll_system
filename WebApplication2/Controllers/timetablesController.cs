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
    public class timetablesController : Controller
    {
        private kursach_pm2Entities db = new kursach_pm2Entities();

        // GET: timetables
        public ActionResult Index()
        {
            var timetable = db.timetable.Include(t => t.users);
            return View(timetable.ToList());
        }

        // GET: timetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable timetable = db.timetable.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // GET: timetables/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.users, "id", "login");
            return View();
        }

        // POST: timetables/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userID,date,workhours")] timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.timetable.Add(timetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.users, "id", "login", timetable.userID);
            return View(timetable);
        }

        // GET: timetables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable timetable = db.timetable.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", timetable.userID);
            return View(timetable);
        }

        // POST: timetables/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userID,date,workhours")] timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", timetable.userID);
            return View(timetable);
        }

        // GET: timetables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable timetable = db.timetable.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // POST: timetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            timetable timetable = db.timetable.Find(id);
            db.timetable.Remove(timetable);
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
