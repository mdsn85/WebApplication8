using Microsoft.AspNet.Identity;
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
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Project);
            return View(payments.ToList());
        }


        public ActionResult IndexByProject(int ProjectId)
        {
            var payments = db.Payments.Where(p=>p.ProjectId == ProjectId).Include(p => p.Project);

            ViewBag.ProjectId = ProjectId;
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            //ViewBag.Code = "test";
            ViewBag.CollectionDate = DateTime.Now;
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Code");
            return View();
        }



        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,Receipt,ModOfPayment,Amount,CollectedBy,CollectionDate,ChqNo,ChqDate,TransBy,DateOfCredit,ToBankHM,ACHM,UserCreate,CreateDate,ProjectId")] Payment payment)
        {
            if (ModelState.IsValid)
            {

                payment.CreateDate = DateTime.Now;
                payment.UserCreate = User.Identity.GetUserName();
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            String errors11 = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                     errors11 = " <br /> " + error.ErrorMessage;
                }
            }
            ViewBag.error = errors11;


            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Code", payment.ProjectId);
            return View(payment);
        }


        // GET: Payments/Create
        public ActionResult CreateByProject(int ProjectId,int? Close)
        {
            ViewBag.close = Close;
            var proj = db.Projects.Find(ProjectId);

            ViewBag.Code = proj.Code;
            ViewBag.ProjectIdVal = proj.ProjectId;
            ViewBag.CollectionDate = DateTime.Now.ToShortDateString();
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Code");



            Project project = db.Projects.Find(ProjectId);


            float totalPayment = 0;
            foreach (var pay in project.Payments)
            {
                totalPayment += pay.Amount;
            }

            ViewBag.totalPayment = totalPayment;
            return View();
        }


        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByProject([Bind(Include = "PosRef,PaymentId,Remarks,Receipt,ModOfPayment,TypeOfPayment,Amount,CollectedBy,CollectionDate,ChqNo,ChqDate,TransBy,DateOfCredit,ToBankHM,ACHM,UserCreate,CreateDate,ProjectId")] Payment payment)
        {
            if (ModelState.IsValid)
            {

                var proj = db.Projects.Find(payment.ProjectId);
                if (proj.Payments.Count == 0) {
                    proj.ProjectStatusId = 2;
                }
               
                payment.CreateDate = DateTime.Now;
                payment.UserCreate = User.Identity.GetUserName();
                db.Payments.Add(payment);
                db.SaveChanges();





                return RedirectToAction("CreateByProject", new { ProjectId = payment.ProjectId , Close = 1 });
            }
            String errors11 = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors11 = " <br /> " + error.ErrorMessage;
                }
            }
            ViewBag.error = errors11;


            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Code", payment.ProjectId);




          

            return View(payment);
        }


        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", payment.ProjectId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,Receipt,ModOfPayment,Amount,CollectedBy,CollectionDate,ChqNo,ChqDate,TransBy,DateOfCredit,ToBankHM,ACHM,UserCreate,CreateDate,ProjectId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", payment.ProjectId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
