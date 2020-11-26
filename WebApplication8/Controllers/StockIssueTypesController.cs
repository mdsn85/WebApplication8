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
    public class StockIssueTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StockIssueTypes
        public ActionResult Index()
        {
            return View(db.StockIssueTypes.ToList());
        }

        // GET: StockIssueTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockIssueType stockIssueType = db.StockIssueTypes.Find(id);
            if (stockIssueType == null)
            {
                return HttpNotFound();
            }
            return View(stockIssueType);
        }

        // GET: StockIssueTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockIssueTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockIssueTypeId,Name,Type")] StockIssueType stockIssueType)
        {
            if (ModelState.IsValid)
            {
                db.StockIssueTypes.Add(stockIssueType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockIssueType);
        }

        // GET: StockIssueTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockIssueType stockIssueType = db.StockIssueTypes.Find(id);
            if (stockIssueType == null)
            {
                return HttpNotFound();
            }
            return View(stockIssueType);
        }

        // POST: StockIssueTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockIssueTypeId,Name,Type")] StockIssueType stockIssueType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockIssueType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockIssueType);
        }

        // GET: StockIssueTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockIssueType stockIssueType = db.StockIssueTypes.Find(id);
            if (stockIssueType == null)
            {
                return HttpNotFound();
            }
            return View(stockIssueType);
        }

        // POST: StockIssueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockIssueType stockIssueType = db.StockIssueTypes.Find(id);
            db.StockIssueTypes.Remove(stockIssueType);
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
