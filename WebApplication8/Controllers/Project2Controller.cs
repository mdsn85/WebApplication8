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
    public class Project2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Project2
        public ActionResult Index()
        {
            var project2 = db.Project2.Include(p => p.Customer).Include(p => p.Designer).Include(p => p.ProjectPaymentTerm).Include(p => p.SalesMan);
            return View(project2.ToList());
        }

        // GET: Project2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project2 project2 = db.Project2.Find(id);
            if (project2 == null)
            {
                return HttpNotFound();
            }
            return View(project2);
        }

        // GET: Project2/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name");
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name");
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name");
            return View();
        }

        // POST: Project2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Project2Id,Name,Code,SalesDate,SalesManId,CustomerId,DesignerId,Value,Discount,Vat,Total,SalePrice,ProjectPaymentTermId,Description,DeliveryDate,ActualDeliveryDate,AccountApproval")] Project2 project2)
        {
            if (ModelState.IsValid)
            {
                db.Project2.Add(project2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", project2.CustomerId);
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name", project2.DesignerId);
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name", project2.ProjectPaymentTermId);
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name", project2.SalesManId);
            return View(project2);
        }

        // GET: Project2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project2 project2 = db.Project2.Find(id);
            if (project2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", project2.CustomerId);
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name", project2.DesignerId);
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name", project2.ProjectPaymentTermId);
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name", project2.SalesManId);
            return View(project2);
        }

        // POST: Project2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Project2Id,Name,Code,SalesDate,SalesManId,CustomerId,DesignerId,Value,Discount,Vat,Total,SalePrice,ProjectPaymentTermId,Description,DeliveryDate,ActualDeliveryDate,AccountApproval")] Project2 project2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", project2.CustomerId);
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name", project2.DesignerId);
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name", project2.ProjectPaymentTermId);
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name", project2.SalesManId);
            return View(project2);
        }

        // GET: Project2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project2 project2 = db.Project2.Find(id);
            if (project2 == null)
            {
                return HttpNotFound();
            }
            return View(project2);
        }

        // POST: Project2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project2 project2 = db.Project2.Find(id);
            db.Project2.Remove(project2);
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
