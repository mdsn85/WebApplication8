using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Controllers.Resources;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class LpoLocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LpoLocations
        public ActionResult Index()
        {
            return View(db.LpoLocations.ToList());
        }

        // GET: LpoLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoLocation lpoLocation = db.LpoLocations.Find(id);
            if (lpoLocation == null)
            {
                return HttpNotFound();
            }
            return View(lpoLocation);
        }

        public JsonResult get()
        {

            List<KeyValuePair> lpoLocation = db.LpoLocations.Select(l => new KeyValuePair { Id = l.LpoLocationId , Name = l.Name }).ToList();
            if (lpoLocation == null)
            {
                return Json(-1);
            }
            return Json(lpoLocation,JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateShort()
        {
            return View();
        }
        [HttpPost]

        public JsonResult CreateShort(string Name)
        {
            LpoLocation lpoLocation = new LpoLocation();
            lpoLocation.Name = Name;
            if (ModelState.IsValid)
            {
                db.LpoLocations.Add(lpoLocation);
                db.SaveChanges();
                return Json( lpoLocation.LpoLocationId);
            }

            return Json(-1);
        }
        // GET: LpoLocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LpoLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LpoLocationId,Name")] LpoLocation lpoLocation)
        {
            if (ModelState.IsValid)
            {
                db.LpoLocations.Add(lpoLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lpoLocation);
        }

        // GET: LpoLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoLocation lpoLocation = db.LpoLocations.Find(id);
            if (lpoLocation == null)
            {
                return HttpNotFound();
            }
            return View(lpoLocation);
        }

        // POST: LpoLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LpoLocationId,Name")] LpoLocation lpoLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lpoLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lpoLocation);
        }

        // GET: LpoLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoLocation lpoLocation = db.LpoLocations.Find(id);
            if (lpoLocation == null)
            {
                return HttpNotFound();
            }
            return View(lpoLocation);
        }

        // POST: LpoLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LpoLocation lpoLocation = db.LpoLocations.Find(id);
            db.LpoLocations.Remove(lpoLocation);
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
