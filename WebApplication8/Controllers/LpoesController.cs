﻿using Microsoft.AspNet.Identity;
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
    public class LpoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private INotificationRepository notificationRepository;


        public LpoesController()
        {
            this.notificationRepository = new NotificationRepository(new ApplicationDbContext());
        }

        // GET: Lpoes
        [Authorize(Roles = RoleNames.ROLE_LPOView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index(string st, string SearchCode, string StatusId, string supplierId, string SearchValue2 , string StartDate, string EndDate)
        {

            ViewBag.title1 = "LPO List";


            ViewBag.SearchCode = SearchCode;

            ViewBag.StatusId = new SelectList(db.LpoStatus, "LpoStatusId", "Name", StatusId);

            ViewBag.supplierId = new SelectList(db.suppliers, "supplierId", "Name", supplierId);

            ViewBag.StartDate = StartDate;
            ViewBag.endDate = EndDate;

            List<Lpo> lpoes = db.Lpoes.Include(l => l.CreditTermSupplier).Include(l => l.Supplier).ToList();


            if (!String.IsNullOrEmpty(SearchCode))
            {
                lpoes = lpoes.Where(s => s.code.ToString().Contains(SearchCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            if (!String.IsNullOrEmpty(StatusId))
            {
                int qqqq = int.Parse(StatusId);
                lpoes = lpoes.Where(s => s.LpoStatusId != null && s.LpoStatusId == qqqq).ToList();
            }

            if (!String.IsNullOrEmpty(supplierId))
            {
                int qqqq = int.Parse(supplierId);
                lpoes = lpoes.Where(s => s.SupplierId != null && s.SupplierId == qqqq).ToList();
            }


            if (!String.IsNullOrEmpty(StartDate))
            {
                if (!String.IsNullOrEmpty(EndDate))
                {
                    DateTime ddStart = DateTime.ParseExact(StartDate, "yyyy-MM-dd", null);
                    DateTime ddEnd = DateTime.ParseExact(EndDate, "yyyy-MM-dd", null);
                    ddEnd = ddEnd.AddDays(1);
                    lpoes = lpoes.Where(cb => cb.LpoDate >= ddStart && cb.LpoDate < ddEnd).ToList();
                }
                else
                {
                    //on selected day
                    DateTime ddStart = DateTime.ParseExact(StartDate, "yyyy-MM-dd", null);
                    DateTime ddEnd = ddStart.AddDays(1);
                    lpoes = lpoes.Where(cb => cb.LpoDate >= ddStart && cb.LpoDate < ddEnd).ToList();
                }
            }

            lpoes = lpoes.OrderByDescending(p => p.StampDate).ToList();

            return View(lpoes.ToList());
        }

        // GET: Lpoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lpo lpo = db.Lpoes.Find(id);
            if (lpo == null)
            {
                return HttpNotFound();
            }
            return View(lpo);
        }

        [Authorize(Roles = RoleNames.ROLE_LPOPrint + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult PrintNewTable(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lpo lpo = db.Lpoes.Find(id);

            if (lpo.LpoStatusId ==2)//revised
            {
                string message = "Sorry ,  You cant print <br/> " +
                    " <br/> " +
                    "Contact manaegment ";

                return ErrorPermission(message);
            }

            ViewBag.LpoTerms = db.LpoTerms.ToList();

            return View(lpo);

        }

        [Authorize(Roles = RoleNames.ROLE_LPOPrint + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult PrintNewTableWithLetterHead(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lpo lpo = db.Lpoes.Find(id);

            if (lpo.LpoStatusId == 2)//revised
            {
                string message = "Sorry ,  You cant print <br/> " +
                    " <br/> " +
                    "Contact manaegment ";

                return ErrorPermission(message);
            }

            ViewBag.LpoTerms = db.LpoTerms.ToList();
            var username = User.Identity.GetUserName();

            ViewBag.user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            var emp = db.EmployeeUsers.Where(eu => eu.User == username).FirstOrDefault().EmployeeId;
            ViewBag.currentEmployee = db.Employees.Find(emp);
            string job = "";
            DateTime currentDate = DateTime.Now;
            int JobBySupplier = db.Lpoes.Where(l => l.Supplier.supplierId == lpo.SupplierId     
                                                && l.LpoDate.Month == currentDate.Month && lpo.LpoDate.Year == currentDate.Year).Count();
            job = string.Format("{0}/{1}/job {2}", currentDate.Year, currentDate.Month, JobBySupplier);
            ViewBag.Job = job ;
            return View(lpo);

        }

        [Authorize(Roles = RoleNames.ROLE_LPOPrint + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult PrintNewTableWithLetterHead2(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lpo lpo = db.Lpoes.Find(id);

            if (lpo.LpoStatusId == 2)//revised
            {
                string message = "Sorry ,  You cant print <br/> " +
                    " <br/> " +
                    "Contact manaegment ";

                return ErrorPermission(message);
            }

            ViewBag.LpoTerms = db.LpoTerms.ToList();

            return View(lpo);

        }


        public ActionResult ErrorPermission(string message)
        {
            ViewBag.Message = message;
            return View("ErrorPermission");
        }



        public JsonResult LpoDetails(int Lpoid)
        {
            var l = db.Lpoes.Find(Lpoid);
           var ld = l.LpoDetails.Select(a => new
            {
                MaterialId = a.MaterialId,
                Name = a.Material.Name,
                Unit = a.Material.Unit.Name,
               type = a.Material.MaterialType.Name,
               Qty = a.Qty,
               Price = a.Price,


           }).ToList();
            return Json(ld, JsonRequestBehavior.AllowGet);
        }


        public int GetSeq()
        {
            int n = 0;
            try
            {
                n = db.LpoSequense.Max(c => c.Sequense) ;
                if (n < 1000)
                {
                    n = 1001;
                }
                n++;

            }
            catch (Exception)
            {
                n = 1001;
            }

            LpoSequense ls = new LpoSequense();
            ls.Sequense = n;
            db.LpoSequense.Add(ls);
            db.SaveChanges();
            return n;
        }

        public String generateContractCode(int seq)
        {
            String s1 = "MRHM_PO_";
            //String y = DateTime.Now.Year.ToString(); ;
            //s1 += y;
            // String S2 = " - CT";

            string S2 = seq.ToString();

            String s = s1 + S2;
            
            return s;
        }

        // GET: Lpoes/Create

        [Authorize(Roles = RoleNames.ROLE_LPOCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            ViewBag.title1 = "Create LPO";

            int Sequense = GetSeq();
            ViewBag.code = generateContractCode(Sequense);

    
            ViewBag.CreditTermSupplierId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name");
            ViewBag.SupplierId = new SelectList(db.suppliers, "supplierId", "Name");
            ViewBag.LpoLocationId = new SelectList(db.LpoLocations, "LpoLocationId", "Name");
            ViewBag.LpoNatureId = new SelectList(db.LpoNatures, "LpoNatureId", "Name");

            ViewBag.LpoDate = DateTime.Now;
            return View();
        }

        // GET: Lpoes/Create
        private int NumberOfRevised(string lpoBaseCode)
        {
            return db.Lpoes.Where(l => l.code.Contains(lpoBaseCode)).Count()-1;
        }
        [Authorize(Roles = RoleNames.ROLE_LPORevise + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Revised(int id )
        {
            Lpo lpo = db.Lpoes.Find(id);
            string lpoBaseCode = (lpo.code.Contains("-Rev")) ? lpo.code.Substring(0, lpo.code.IndexOf("-Rev")) : lpo.code;

            string newCode = lpoBaseCode + "-Rev" + (NumberOfRevised(lpoBaseCode)+1).ToString("000");
            ViewBag.code = newCode;


            //ViewBag.CreditTermId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name");

            ViewBag.CreditTermSupplierId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name", lpo.CreditTermSupplierId);
            ViewBag.SupplierId = new SelectList(db.suppliers, "supplierId", "Name",lpo.SupplierId);
            ViewBag.LpoLocationId = new SelectList(db.LpoLocations, "LpoLocationId", "Name",lpo.LpoLocationId);
            ViewBag.LpoNatureId = new SelectList(db.LpoNatures, "LpoNatureId", "Name", lpo.LpoNatureId);
            ViewBag.LpoDate = DateTime.Now;

            return View(lpo);
        }

        // POST: Lpoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LpoId,code,SupplierRef,LpoDate,SupplierId,CreditTermId,QuotationRef")] Lpo lpo)
        {
            if (ModelState.IsValid)
            {

                lpo.StampDate = DateTime.Now;
                db.Lpoes.Add(lpo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreditTermSupplierId = new SelectList(db.CreditTermSuppliers, "CreditTermSupplierId", "Name", lpo.CreditTermSupplierId);
            ViewBag.SupplierId = new SelectList(db.suppliers, "supplierId", "Name", lpo.SupplierId);
            ViewBag.LpoDate = lpo.LpoDate;
            return View(lpo);
        }


        public JsonResult CreateJson([Bind(Include = "LpoId,code,SupplierRef,LpoDate,SupplierId,CreditTermSupplierId,SubTotal,Discount,Total,Vat,GrandTotal,Remarks,LpoLocationId,LpoNatureId ")] Lpo lpo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(lpo.LpoId != 0)
                    {
                        var old = db.Lpoes.Find(lpo.LpoId);
                        old.LpoStatusId = 2;//revised
                    }

                    lpo.StampDate = DateTime.Now;

                    // OldLPO = db.Lpoes.Find()
                    lpo.LpoStatusId = 1;//pending 
                    lpo.UserCreate = User.Identity.GetUserName();
                    db.Lpoes.Add(lpo);
                    db.SaveChanges();

                    this.notificationRepository.CreateNotificationAsync(lpo.LpoId, NotificationName.onCreateLpo);
                    return Json(lpo.LpoId);
                }
                catch (Exception ex)
                {
                    String errors11 = ex.Message;

                    if (ex.InnerException != null)
                    {
                        errors11 = errors11 + ex.InnerException.Message + " \r\n "; ;
                    }
                    return Json(errors11);
                }
            }
            return Json("Error");
        }

        [HttpPost]
        public JsonResult CreateDetailsJson(List<LpoDetail> Details)
        {
            string errors11 = "";
            int aaa = 0;
            int test = 0;
            int eid = 0;
            LpoDetail EP = new LpoDetail();
            foreach (LpoDetail d in Details)
            {
                try
                {
                    EP = new LpoDetail();
                    EP.LpoId = int.Parse(d.LpoId.ToString());
                    eid = int.Parse(d.LpoId.ToString());
                    EP.MaterialId = int.Parse(d.MaterialId.ToString());
                    EP.Qty = float.Parse(d.Qty.ToString());
                    EP.Price = float.Parse(d.Price.ToString());

                    db.LpoDetails.Add(EP);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    DeleteConfirmed(eid);
                    test = 1;
                    errors11 = errors11 + ex.Message;
                    if (ex.InnerException != null)
                    {

                        if (ex.InnerException.InnerException != null)
                        {
                            errors11 = errors11 + ex.InnerException.InnerException.Message;
                        }
                    }
                    return Json(errors11);
                }

            }
            if (test == 1)
            {
                return Json(errors11);
            }
            return Json("Cutting Sheet Saved Successfully");

        }

        // GET: Lpoes/Edit/5
        [Authorize(Roles = RoleNames.ROLE_LPOEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lpo lpo = db.Lpoes.Find(id);
            if (lpo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreditTermId = new SelectList(db.CreditTerms, "CreditTermId", "Name", lpo.CreditTermSupplierId);
            ViewBag.SupplierId = new SelectList(db.suppliers, "supplierId", "Name", lpo.SupplierId);
            return View(lpo);
        }

        // POST: Lpoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LpoId,code,SupplierRef,LpoDate,SupplierId,CreditTermSupplierId")] Lpo lpo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lpo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreditTermId = new SelectList(db.CreditTerms, "CreditTermId", "Name", lpo.CreditTermSupplierId);
            ViewBag.SupplierId = new SelectList(db.suppliers, "supplierId", "Name", lpo.SupplierId);
            return View(lpo);
        }

        // GET: Lpoes/Delete/5
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lpo lpo = db.Lpoes.Find(id);
            if (lpo == null)
            {
                return HttpNotFound();
            }
            return View(lpo);
        }

        // POST: Lpoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lpo lpo = db.Lpoes.Find(id);
            db.Lpoes.Remove(lpo);
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
