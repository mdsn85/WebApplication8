using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;
using WebApplication8.Models.Fillter;
using WebApplication8.Models.Repository;
using WebApplication8.Persistence;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace WebApplication8.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IInvoiceRepository invoiceRepository;

        public InvoicesController()
        {
            this.invoiceRepository = new InvoiceRepository(new ApplicationDbContext());
        }

        // GET: Invoices
        [Authorize(Roles = RoleNames.ROLE_InvoiceView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index([Bind(Include = "SearchCode, CustomerId, ProjectId,  StartDate,  EndDate")] InvoiceFillter fillter)
        {
            ViewBag.SearchCode = fillter.SearchCode;
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", fillter.CustomerId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Code", fillter.ProjectId);
            ViewBag.StartDate = fillter.StartDate;
            ViewBag.endDate = fillter.EndDate;

            var invoice = invoiceRepository.GetAll(fillter);
            return View(invoice.ToList());
        }

        // GET: Invoices/Details/5
        [Authorize(Roles = RoleNames.ROLE_InvoiceView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = invoiceRepository.Get(id??0);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [Authorize(Roles = RoleNames.ROLE_InvoicePrint + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult PrintNewTableWithLetterHead(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invoice = invoiceRepository.Get(id??0);



            var username = User.Identity.GetUserName();

            ViewBag.user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            var emp = db.EmployeeUsers.Where(eu => eu.User == username).FirstOrDefault().EmployeeId;
            ViewBag.currentEmployee = db.Employees.Find(emp);
            string job = "";
            DateTime currentDate = DateTime.Now;

            return View(invoice);

        }

        public int GetSeq()
        {
            int? n = 0;
            try
            {
                n = db.Invoices.Max(c => c.Sequense);
                if (n < 1000 || n==null)
                {
                    n = 1001;
                }
                n++;
            }
            catch (Exception)
            {
                n = 1001;
            }

            InvoiceSequense ls = new InvoiceSequense();
            ls.Sequense = n??1000;
            db.InvoiceSequenses.Add(ls);
            db.SaveChanges();
            return ls.Sequense;
        }

        public String generateContractCode(int seq)
        {
            string s1 = "MRHM_INV_";

            string S2 = seq.ToString();

            String s = s1 + S2;

            return s;
        }

        // GET: Invoices/Create
        [Authorize(Roles = RoleNames.ROLE_InvoiceCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }

        [Authorize(Roles = RoleNames.ROLE_InvoiceCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult CreateFromProject(int CustomerId, int ProjectId,string CustomerName,string Code)
        {
            Invoice invoice = new Invoice();
            invoice.CustomerId = CustomerId;
            invoice.ProjectId = ProjectId;

            invoice.Sequense = GetSeq();
            invoice.Code = generateContractCode(invoice.Sequense??1000);
            ViewBag.code = invoice.Code;
            invoice.BankDetails = invoiceRepository.getBank();
            invoice.Declaration = invoiceRepository.getDeclaration();

            ViewBag.CustomerName = CustomerName;
            ViewBag.ProjectCode = Code;
            return View(invoice);
        }
        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public JsonResult GetInvoiceJson([Bind(Include = "InvoiceId,Code,InvoiceDate,Sequense,CreateDate,DeliverNote,DespatchedThrough,ProjectId,CustomerId,CustomerReference,Total,Discount,Vat,SubTotal,GrandTotal,TermsOfDelivery,Remarks,BankDetails,Declaration")] Invoice invoice)
        {
            return Json(invoice, JsonRequestBehavior.AllowGet);
        }


    [HttpPost]
        [Authorize]
        public JsonResult CreateJson(Invoice invoice,List<InvoiceProduct> Details)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                invoice.UserCreate = User.Identity.GetUserName();

                invoiceRepository.Add(invoice, Details);
                invoiceRepository.Save();
                return Json(true);
            }
            else
            {
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorMessage += " \r\n " + error.ErrorMessage ;
                    }
                }
                return Json(errorMessage);
            }

        }



        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", invoice.CustomerId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", invoice.ProjectId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,Code,InvoiceDate,CreateDate,DeliverNote,DespatchedThrough,ProjectId,CustomerId,CustomerReference,Total,Discount,Vat,TotalNet,TermsOfDelivery,Remarks,BankDetails,Declaration")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", invoice.CustomerId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", invoice.ProjectId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = invoiceRepository.Get(id??0);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            invoiceRepository.Delete(id);
            invoiceRepository.Save(); ;
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            //invoiceRepository.Dispose();
           // base.Dispose(disposing);
        }
    }
}
