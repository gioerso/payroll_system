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
    public class paynamentMethodsController : Controller
    {
        private kursach_pm2Entities db = new kursach_pm2Entities();

        // GET: paynamentMethods
        public ActionResult Index()
        {
            var paynamentMethods = db.paynamentMethods.Include(p => p.users);
            return View(paynamentMethods.ToList());
        }

        // GET: paynamentMethods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paynamentMethods paynamentMethods = db.paynamentMethods.Find(id);
            if (paynamentMethods == null)
            {
                return HttpNotFound();
            }
            return View(paynamentMethods);
        }

        // GET: paynamentMethods/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.users, "id", "login");
            return View();
        }

        // POST: paynamentMethods/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,BIK,INN,korr,AccNum")] paynamentMethods paynamentMethods)
        {
            if (ModelState.IsValid)
            {
                db.paynamentMethods.Add(paynamentMethods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.users, "id", "login", paynamentMethods.userID);
            return View(paynamentMethods);
        }

        // GET: paynamentMethods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paynamentMethods paynamentMethods = db.paynamentMethods.Find(id);
            if (paynamentMethods == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", paynamentMethods.userID);
            return View(paynamentMethods);
        }

        // POST: paynamentMethods/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,BIK,INN,korr,AccNum")] paynamentMethods paynamentMethods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paynamentMethods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.users, "id", "login", paynamentMethods.userID);
            return View(paynamentMethods);
        }

        // GET: paynamentMethods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paynamentMethods paynamentMethods = db.paynamentMethods.Find(id);
            if (paynamentMethods == null)
            {
                return HttpNotFound();
            }
            return View(paynamentMethods);
        }

        // POST: paynamentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paynamentMethods paynamentMethods = db.paynamentMethods.Find(id);
            db.paynamentMethods.Remove(paynamentMethods);
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
