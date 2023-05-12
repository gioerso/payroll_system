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
    public class awardsController : Controller
    {
        private kursach_pm2Entities db = new kursach_pm2Entities();

        // GET: awards
        public ActionResult Index()
        {
            var awards = db.awards.Include(a => a.users);
            return View(awards.ToList());
        }

        // GET: awards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            awards awards = db.awards.Find(id);
            if (awards == null)
            {
                return HttpNotFound();
            }
            return View(awards);
        }

        // GET: awards/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.users, "id", "login");
            return View();
        }

        // POST: awards/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,userID,month,awardPercent")] awards awards)
        {
            if (ModelState.IsValid)
            {
                db.awards.Add(awards);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.users, "id", "login", awards.userID);
            return View(awards);
        }

        // GET: awards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            awards awards = db.awards.Find(id);
            if (awards == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", awards.userID);
            return View(awards);
        }

        // POST: awards/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,userID,month,awardPercent")] awards awards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(awards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", awards.userID);
            return View(awards);
        }

        // GET: awards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            awards awards = db.awards.Find(id);
            if (awards == null)
            {
                return HttpNotFound();
            }
            return View(awards);
        }

        // POST: awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            awards awards = db.awards.Find(id);
            db.awards.Remove(awards);
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
