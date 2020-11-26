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
    public class DesignersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Designers
        public ActionResult Index()
        {
            var designers = db.Designers.Include(d => d.Employees);
            return View(designers.ToList());
        }

        // GET: Designers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designer designer = db.Designers.Find(id);
            if (designer == null)
            {
                return HttpNotFound();
            }
            return View(designer);
        }

        // GET: Designers/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name");
            return View();
        }

        // POST: Designers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DesignerId,Name,Mobile,Email,EmployeeId")] Designer designer)
        {
            if (ModelState.IsValid)
            {
                var e = db.Employees.Find(designer.EmployeeId);

                designer.Name = e.Name;
                db.Designers.Add(designer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Code", designer.EmployeeId);
            return View(designer);
        }

        // GET: Designers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designer designer = db.Designers.Find(id);
            if (designer == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Code", designer.EmployeeId);
            return View(designer);
        }

        // POST: Designers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignerId,Name,Mobile,Email,EmployeeId")] Designer designer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(designer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Code", designer.EmployeeId);
            return View(designer);
        }

        // GET: Designers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designer designer = db.Designers.Find(id);
            if (designer == null)
            {
                return HttpNotFound();
            }
            return View(designer);
        }

        // POST: Designers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Designer designer = db.Designers.Find(id);
            db.Designers.Remove(designer);
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
