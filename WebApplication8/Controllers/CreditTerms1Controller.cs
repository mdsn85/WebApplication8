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
    public class CreditTerms1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CreditTerms1
        public ActionResult Index()
        {
            return View(db.CreditTerms.ToList());
        }

        // GET: CreditTerms1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditTerm creditTerm = db.CreditTerms.Find(id);
            if (creditTerm == null)
            {
                return HttpNotFound();
            }
            return View(creditTerm);
        }

        // GET: CreditTerms1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreditTerms1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreditTermId,Name")] CreditTerm creditTerm)
        {
            if (ModelState.IsValid)
            {
                db.CreditTerms.Add(creditTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creditTerm);
        }

        // GET: CreditTerms1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditTerm creditTerm = db.CreditTerms.Find(id);
            if (creditTerm == null)
            {
                return HttpNotFound();
            }
            return View(creditTerm);
        }

        // POST: CreditTerms1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CreditTermId,Name")] CreditTerm creditTerm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditTerm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creditTerm);
        }

        // GET: CreditTerms1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditTerm creditTerm = db.CreditTerms.Find(id);
            if (creditTerm == null)
            {
                return HttpNotFound();
            }
            return View(creditTerm);
        }

        // POST: CreditTerms1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditTerm creditTerm = db.CreditTerms.Find(id);
            db.CreditTerms.Remove(creditTerm);
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
