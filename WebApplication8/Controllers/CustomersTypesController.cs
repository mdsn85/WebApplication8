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
    public class CustomersTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomersTypes
        public ActionResult Index()
        {
            return View(db.CustomersTypes.ToList());
        }

        // GET: CustomersTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersType customersType = db.CustomersTypes.Find(id);
            if (customersType == null)
            {
                return HttpNotFound();
            }
            return View(customersType);
        }

        // GET: CustomersTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomersTypeId,Name")] CustomersType customersType)
        {
            if (ModelState.IsValid)
            {
                db.CustomersTypes.Add(customersType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customersType);
        }

        // GET: CustomersTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersType customersType = db.CustomersTypes.Find(id);
            if (customersType == null)
            {
                return HttpNotFound();
            }
            return View(customersType);
        }

        // POST: CustomersTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomersTypeId,Name")] CustomersType customersType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customersType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customersType);
        }

        // GET: CustomersTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersType customersType = db.CustomersTypes.Find(id);
            if (customersType == null)
            {
                return HttpNotFound();
            }
            return View(customersType);
        }

        // POST: CustomersTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomersType customersType = db.CustomersTypes.Find(id);
            db.CustomersTypes.Remove(customersType);
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
