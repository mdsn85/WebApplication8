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
    public class ProjectPaymentTerms3Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectPaymentTerms3
        public ActionResult Index()
        {
            return View(db.ProjectPaymentTerms.ToList());
        }

        // GET: ProjectPaymentTerms3/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectPaymentTerm projectPaymentTerm = db.ProjectPaymentTerms.Find(id);
            if (projectPaymentTerm == null)
            {
                return HttpNotFound();
            }
            return View(projectPaymentTerm);
        }

        // GET: ProjectPaymentTerms3/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectPaymentTerms3/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectPaymentTermId,Name")] ProjectPaymentTerm projectPaymentTerm)
        {
            if (ModelState.IsValid)
            {
                db.ProjectPaymentTerms.Add(projectPaymentTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectPaymentTerm);
        }

        // GET: ProjectPaymentTerms3/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectPaymentTerm projectPaymentTerm = db.ProjectPaymentTerms.Find(id);
            if (projectPaymentTerm == null)
            {
                return HttpNotFound();
            }
            return View(projectPaymentTerm);
        }

        // POST: ProjectPaymentTerms3/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectPaymentTermId,Name")] ProjectPaymentTerm projectPaymentTerm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectPaymentTerm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectPaymentTerm);
        }

        // GET: ProjectPaymentTerms3/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectPaymentTerm projectPaymentTerm = db.ProjectPaymentTerms.Find(id);
            if (projectPaymentTerm == null)
            {
                return HttpNotFound();
            }
            return View(projectPaymentTerm);
        }

        // POST: ProjectPaymentTerms3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectPaymentTerm projectPaymentTerm = db.ProjectPaymentTerms.Find(id);
            db.ProjectPaymentTerms.Remove(projectPaymentTerm);
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
