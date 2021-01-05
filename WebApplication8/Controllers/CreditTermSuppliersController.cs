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
    public class CreditTermSuppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CreditTermSuppliers
        [Authorize(Roles = RoleNames.ROLE_SupplierView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index()
        {
            return View(db.CreditTermSuppliers.ToList());
        }

        // GET: CreditTermSuppliers/Details/5
        [Authorize(Roles = RoleNames.ROLE_SupplierView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditTermSupplier creditTermSupplier = db.CreditTermSuppliers.Find(id);
            if (creditTermSupplier == null)
            {
                return HttpNotFound();
            }
            return View(creditTermSupplier);
        }

        // GET: CreditTermSuppliers/Create
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreditTermSuppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create([Bind(Include = "CreditTermSupplierId,Name")] CreditTermSupplier creditTermSupplier)
        {
            if (ModelState.IsValid)
            {
                db.CreditTermSuppliers.Add(creditTermSupplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(creditTermSupplier);
        }

        // GET: CreditTermSuppliers/Edit/5
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditTermSupplier creditTermSupplier = db.CreditTermSuppliers.Find(id);
            if (creditTermSupplier == null)
            {
                return HttpNotFound();
            }
            return View(creditTermSupplier);
        }

        // POST: CreditTermSuppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_SupplierCreate + "," + RoleNames.ROLE_SupplierEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit([Bind(Include = "CreditTermSupplierId,Name")] CreditTermSupplier creditTermSupplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditTermSupplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creditTermSupplier);
        }

        // GET: CreditTermSuppliers/Delete/5
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditTermSupplier creditTermSupplier = db.CreditTermSuppliers.Find(id);
            if (creditTermSupplier == null)
            {
                return HttpNotFound();
            }
            return View(creditTermSupplier);
        }

        // POST: CreditTermSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditTermSupplier creditTermSupplier = db.CreditTermSuppliers.Find(id);
            db.CreditTermSuppliers.Remove(creditTermSupplier);
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
