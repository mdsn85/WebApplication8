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
    public class LpoStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LpoStatus
        public ActionResult Index()
        {
            return View(db.LpoStatus.ToList());
        }

        // GET: LpoStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoStatus lpoStatus = db.LpoStatus.Find(id);
            if (lpoStatus == null)
            {
                return HttpNotFound();
            }
            return View(lpoStatus);
        }

        // GET: LpoStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LpoStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LpoStatusId,Name")] LpoStatus lpoStatus)
        {
            if (ModelState.IsValid)
            {
                db.LpoStatus.Add(lpoStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lpoStatus);
        }

        // GET: LpoStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoStatus lpoStatus = db.LpoStatus.Find(id);
            if (lpoStatus == null)
            {
                return HttpNotFound();
            }
            return View(lpoStatus);
        }

        // POST: LpoStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LpoStatusId,Name")] LpoStatus lpoStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lpoStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lpoStatus);
        }

        // GET: LpoStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoStatus lpoStatus = db.LpoStatus.Find(id);
            if (lpoStatus == null)
            {
                return HttpNotFound();
            }
            return View(lpoStatus);
        }

        // POST: LpoStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LpoStatus lpoStatus = db.LpoStatus.Find(id);
            db.LpoStatus.Remove(lpoStatus);
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
