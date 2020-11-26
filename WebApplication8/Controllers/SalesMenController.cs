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
    public class SalesMenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SalesMen
        public ActionResult Index()
        {
            var salesMen = db.SalesMen.Include(s => s.Employees);
            return View(salesMen.ToList());
        }

        // GET: SalesMen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMan salesMan = db.SalesMen.Find(id);
            if (salesMan == null)
            {
                return HttpNotFound();
            }
            return View(salesMan);
        }

        // GET: SalesMen/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name");
            return View();
        }

        // POST: SalesMen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesManId,Name,Mobile,Email,EmployeeId")] SalesMan salesMan)
        {
            if (ModelState.IsValid)
            {
                var e = db.Employees.Find(salesMan.EmployeeId);

                salesMan.Name = e.Name;
                db.SalesMen.Add(salesMan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", salesMan.EmployeeId);
            return View(salesMan);
        }

        // GET: SalesMen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMan salesMan = db.SalesMen.Find(id);
            if (salesMan == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Code", salesMan.EmployeeId);
            return View(salesMan);
        }

        // POST: SalesMen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesManId,Name,Mobile,Email,EmployeeId")] SalesMan salesMan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesMan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Code", salesMan.EmployeeId);
            return View(salesMan);
        }

        // GET: SalesMen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMan salesMan = db.SalesMen.Find(id);
            if (salesMan == null)
            {
                return HttpNotFound();
            }
            return View(salesMan);
        }

        // POST: SalesMen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesMan salesMan = db.SalesMen.Find(id);
            db.SalesMen.Remove(salesMan);
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
