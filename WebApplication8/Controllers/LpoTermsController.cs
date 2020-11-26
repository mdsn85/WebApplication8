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
    public class LpoTermsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LpoTerms
        public ActionResult Index()
        {
            return View(db.LpoTerms.ToList());
        }

        // GET: LpoTerms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoTerms lpoTerms = db.LpoTerms.Find(id);
            if (lpoTerms == null)
            {
                return HttpNotFound();
            }
            return View(lpoTerms);
        }

        // GET: LpoTerms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LpoTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LpoTermsId,Text")] LpoTerms lpoTerms)
        {
            if (ModelState.IsValid)
            {
                db.LpoTerms.Add(lpoTerms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lpoTerms);
        }

        // GET: LpoTerms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoTerms lpoTerms = db.LpoTerms.Find(id);
            if (lpoTerms == null)
            {
                return HttpNotFound();
            }
            return View(lpoTerms);
        }

        // POST: LpoTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LpoTermsId,Text")] LpoTerms lpoTerms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lpoTerms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lpoTerms);
        }

        // GET: LpoTerms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoTerms lpoTerms = db.LpoTerms.Find(id);
            if (lpoTerms == null)
            {
                return HttpNotFound();
            }
            return View(lpoTerms);
        }

        // POST: LpoTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LpoTerms lpoTerms = db.LpoTerms.Find(id);
            db.LpoTerms.Remove(lpoTerms);
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
