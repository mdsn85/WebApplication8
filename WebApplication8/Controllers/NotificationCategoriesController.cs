using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class NotificationCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NotificationCategories
        public ActionResult Index()
        {
            return View(db.NotificationCategorys.ToList());
        }

        // GET: NotificationCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationCategory notificationCategory = db.NotificationCategorys.Find(id);
            if (notificationCategory == null)
            {
                return HttpNotFound();
            }
            return View(notificationCategory);
        }

        // GET: NotificationCategories/Create
        public ActionResult Create()
        {

            ViewBag.Users = db.Users.Select(u=> new { label = u.UserName, value = u.Id }).ToList();
            return View();
        }

        // POST: NotificationCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NotificationCategoryId,Name,Title,Details,DetailsURL,NotificationType")] NotificationCategory notificationCategory, string[] users)
        {
            if (ModelState.IsValid)
            {
                db.NotificationCategorys.Add(notificationCategory);

                NotificationCatUser nu = new NotificationCatUser();
                foreach (string user in users)
                {
                    nu = new NotificationCatUser();
                    nu.UserId = user;
                    nu.NotificationCategoryId = notificationCategory.NotificationCategoryId;
                    db.NotificationCatUsers.Add(nu);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notificationCategory);
        }

        // GET: NotificationCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationCategory notificationCategory = db.NotificationCategorys.Find(id);
            if (notificationCategory == null)
            {
                return HttpNotFound();
            }
            return View(notificationCategory);
        }

        // POST: NotificationCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NotificationCategoryId,Name,Title,Details,DetailsURL,NotificationType")] NotificationCategory notificationCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificationCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notificationCategory);
        }

        // GET: NotificationCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificationCategory notificationCategory = db.NotificationCategorys.Find(id);
            if (notificationCategory == null)
            {
                return HttpNotFound();
            }
            return View(notificationCategory);
        }

        // POST: NotificationCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NotificationCategory notificationCategory = db.NotificationCategorys.Find(id);
            db.NotificationCategorys.Remove(notificationCategory);
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
