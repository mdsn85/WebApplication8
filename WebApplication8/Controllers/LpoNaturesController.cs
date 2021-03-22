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
    public class LpoNaturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LpoNatures
        public ActionResult Index()
        {
            return View(db.LpoNatures.ToList());
        }

        // GET: LpoNatures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoNature lpoNature = db.LpoNatures.Find(id);
            if (lpoNature == null)
            {
                return HttpNotFound();
            }
            return View(lpoNature);
        }


        public JsonResult get()
        {

            List<KeyValuePair> lpoNature = db.LpoNatures.Select(l => new KeyValuePair { Id = l.LpoNatureId, Name = l.Name }).ToList();
            if (lpoNature == null)
            {
                return Json(-1);
            }
            return Json(lpoNature, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateShort()
        {
            return View();
        }


        [HttpPost]
        public JsonResult CreateShort(string Name)
        {
            LpoNature lpoNature = new LpoNature();
            lpoNature.Name = Name;
            if (ModelState.IsValid)
            {
                db.LpoNatures.Add(lpoNature);
                db.SaveChanges();
                return Json(lpoNature.LpoNatureId);
            }

            return Json(-1);
        }

        // GET: LpoNatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LpoNatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LpoNatureId,Name")] LpoNature lpoNature)
        {
            if (ModelState.IsValid)
            {
                db.LpoNatures.Add(lpoNature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lpoNature);
        }

        // GET: LpoNatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoNature lpoNature = db.LpoNatures.Find(id);
            if (lpoNature == null)
            {
                return HttpNotFound();
            }
            return View(lpoNature);
        }

        // POST: LpoNatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LpoNatureId,Name")] LpoNature lpoNature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lpoNature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lpoNature);
        }

        // GET: LpoNatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LpoNature lpoNature = db.LpoNatures.Find(id);
            if (lpoNature == null)
            {
                return HttpNotFound();
            }
            return View(lpoNature);
        }

        // POST: LpoNatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LpoNature lpoNature = db.LpoNatures.Find(id);
            db.LpoNatures.Remove(lpoNature);
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
