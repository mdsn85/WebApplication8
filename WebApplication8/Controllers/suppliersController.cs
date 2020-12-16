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
    public class suppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: suppliers
        [Authorize(Roles = RoleNames.ROLE_SupplierView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index()
        {
            return View(db.suppliers.ToList());
        }

        // GET: suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        public int GetSupplierTerm(int? id)
        {
            if (id == null)
            {
                return 0;
            }
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null || supplier.CreditTermSupplier==null)
            {
                return 0;
            }
            return supplier.CreditTermSupplier.CreditTermSupplierId;
        }

        // GET: suppliers/Create
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            ViewBag.CreditTermSupplierId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name");
            return View();
        }

        // POST: suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "supplierId,Name,Attention,tel,mobile,email,Address,CreditTermSupplierId")] supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreditTermSupplierId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name", supplier.CreditTermSupplierId);
            return View(supplier);
        }

        // GET: suppliers/Edit/5
        [Authorize(Roles = RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreditTermSupplierId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name", supplier.CreditTermSupplierId);
            return View(supplier);
        }

        // POST: suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "supplierId,Name,Attention,tel,mobile,email,Address,CreditTermSupplierId")] supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreditTermSupplierId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name", supplier.CreditTermSupplierId);
            return View(supplier);
        }

        // GET: suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =  RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {
            supplier supplier = db.suppliers.Find(id);
            db.suppliers.Remove(supplier);
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
