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
    public class PermissionUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PermissionUsers
        public ActionResult Index()
        {
            var permissionUser = db.PermissionUsers.Include(p => p.Permissions);
            return View(permissionUser.ToList());
        }

        // GET: PermissionUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionUser permissionUser = db.PermissionUsers.Find(id);
            if (permissionUser == null)
            {
                return HttpNotFound();
            }
            return View(permissionUser);
        }

        // GET: PermissionUsers/Create
        public ActionResult Create()
        {
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "Name");

            ViewBag.UserName = new SelectList(db.Users, "UserName", "UserName");
            return View();
        }

        // POST: PermissionUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermissionUserId,PermissionId,Username")] PermissionUser permissionUser)
        {
            if (ModelState.IsValid)
            {
                db.PermissionUsers.Add(permissionUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "Name", permissionUser.PermissionId);
            return View(permissionUser);
        }

        // GET: PermissionUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionUser permissionUser = db.PermissionUsers.Find(id);
            if (permissionUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "Name", permissionUser.PermissionId);
            return View(permissionUser);
        }

        // POST: PermissionUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermissionUserId,PermissionId,Username")] PermissionUser permissionUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permissionUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PermissionId = new SelectList(db.Permissions, "PermissionId", "Name", permissionUser.PermissionId);
            return View(permissionUser);
        }

        // GET: PermissionUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PermissionUser permissionUser = db.PermissionUsers.Find(id);
            if (permissionUser == null)
            {
                return HttpNotFound();
            }
            return View(permissionUser);
        }

        // POST: PermissionUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PermissionUser permissionUser = db.PermissionUsers.Find(id);
            db.PermissionUsers.Remove(permissionUser);
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
