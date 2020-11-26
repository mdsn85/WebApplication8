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
    public class SalesTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SalesTypes
        public ActionResult Index()
        {
            return View(db.SalesTypes.ToList());
        }

        // GET: SalesTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesType salesType = db.SalesTypes.Find(id);
            if (salesType == null)
            {
                return HttpNotFound();
            }
            return View(salesType);
        }

        // GET: SalesTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesTypeId,Name")] SalesType salesType)
        {
            if (ModelState.IsValid)
            {
                db.SalesTypes.Add(salesType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salesType);
        }

        // GET: SalesTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesType salesType = db.SalesTypes.Find(id);
            if (salesType == null)
            {
                return HttpNotFound();
            }
            return View(salesType);
        }

        // POST: SalesTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesTypeId,Name")] SalesType salesType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesType);
        }

        // GET: SalesTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesType salesType = db.SalesTypes.Find(id);
            if (salesType == null)
            {
                return HttpNotFound();
            }
            return View(salesType);
        }

        // POST: SalesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesType salesType = db.SalesTypes.Find(id);
            db.SalesTypes.Remove(salesType);
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
