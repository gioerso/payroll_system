using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private kursach_pm2Entities db = new kursach_pm2Entities();
        [Authorize]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pay()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Pay(string Id, string Sum, string Date)
        {
            if (ModelState.IsValid)
            {
                if(Id == null && Sum == null)
                {
                    var list = db.accurals.Include(a => a.users).ToList();
                    foreach(accurals l in list){
                        try
                        {
                            int _id = l.id;
                            db.Database.ExecuteSqlCommand($"INSERT INTO accurals(userID, sum, date) VALUES ('{_id}', '30000', '{Date}')");
                        }
                        catch(Exception ex) { Console.WriteLine(ex); }
                    }
                }
                else
                {
                    db.Database.ExecuteSqlCommand($"INSERT INTO accurals(userID, sum, date) VALUES ('{Id}', '{Sum}', '{Date}')");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddTime()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTime(string Date, string Time)
        {
            if (ModelState.IsValid)
            {
                string email = User.Identity.GetUserName();
                // INSERT INTO timetable(userID,date, workhours) VALUES ((SELECT id from users where email = 'admin@admin.com'), '06-12-2022', 9)
                db.Database.ExecuteSqlCommand($"INSERT INTO timetable(userID,date, workhours) VALUES ((SELECT id from users where email = '{email}'), '{Date}', '{int.Parse(Time)}')");
            }
            return RedirectToAction("Index");
        }
        public ActionResult SetAward()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetAward(string userID, string month, string percent)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand($"INSERT INTO awards(userID, [month], [awardPercent]) VALUES ('{userID}', '{month}', {int.Parse(percent)})");
            }
            return RedirectToAction("Index");
        }
        public ActionResult GetMeAccurals()
        {
            var accurals = from s in db.accurals select s;
            string name = User.Identity.GetUserName();
            accurals = accurals.Where(s => s.users.email.Contains(name));
            return View(accurals.ToList());
        }
    }
}
