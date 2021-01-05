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
    public class PaymentMoodSuppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentMoodSuppliers
        [Authorize(Roles = RoleNames.ROLE_SupplierView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index()
        {
            return View(db.PaymentMoodSuppliers.ToList());
        }

        // GET: PaymentMoodSuppliers/Details/5
        [Authorize(Roles = RoleNames.ROLE_SupplierView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMoodSupplier paymentMoodSupplier = db.PaymentMoodSuppliers.Find(id);
            if (paymentMoodSupplier == null)
            {
                return HttpNotFound();
            }
            return View(paymentMoodSupplier);
        }

        // GET: PaymentMoodSuppliers/Create
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentMoodSuppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create([Bind(Include = "PaymentMoodSupplierId,Name")] PaymentMoodSupplier paymentMoodSupplier)
        {
            if (ModelState.IsValid)
            {
                db.PaymentMoodSuppliers.Add(paymentMoodSupplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentMoodSupplier);
        }

        // GET: PaymentMoodSuppliers/Edit/5
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMoodSupplier paymentMoodSupplier = db.PaymentMoodSuppliers.Find(id);
            if (paymentMoodSupplier == null)
            {
                return HttpNotFound();
            }
            return View(paymentMoodSupplier);
        }

        // POST: PaymentMoodSuppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit([Bind(Include = "PaymentMoodSupplierId,Name")] PaymentMoodSupplier paymentMoodSupplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentMoodSupplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentMoodSupplier);
        }

        // GET: PaymentMoodSuppliers/Delete/5
        [Authorize(Roles =  RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMoodSupplier paymentMoodSupplier = db.PaymentMoodSuppliers.Find(id);
            if (paymentMoodSupplier == null)
            {
                return HttpNotFound();
            }
            return View(paymentMoodSupplier);
        }

        // POST: PaymentMoodSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =  RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentMoodSupplier paymentMoodSupplier = db.PaymentMoodSuppliers.Find(id);
            db.PaymentMoodSuppliers.Remove(paymentMoodSupplier);
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
