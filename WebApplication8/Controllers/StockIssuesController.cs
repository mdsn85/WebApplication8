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
    public class StockIssuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        private INotificationRepository notificationRepository;


        public StockIssuesController()
        {
            this.notificationRepository = new NotificationRepository(new ApplicationDbContext());
        }
        // GET: StockIssues
        [Authorize(Roles = RoleNames.ROLE_StockIssuesView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index(string SearchType)
        {
            ViewBag.title1 = "Stock Issues List";
            ViewBag.StockIssueTypeId = new SelectList(db.StockIssueTypes, "StockIssueTypeId", "Name", SearchType);
            List<StockIssue> stockIssues = db.StockIssues.Include(s => s.StockIssueType).Include(s => s.Warehouse).ToList();


            if (!String.IsNullOrEmpty(SearchType))
            {
                int q = int.Parse(SearchType);
                stockIssues = stockIssues.Where(s => s.StockIssueTypeId==q).ToList();
            }


            stockIssues = stockIssues.OrderByDescending(p => p.StampDate).ToList();


            return View(stockIssues.ToList());
        }

        // GET: StockIssues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockIssue stockIssue = db.StockIssues.Find(id);
            if (stockIssue == null)
            {
                return HttpNotFound();
            }
            return View(stockIssue);
        }


        public int GetSeq()
        {
            int n = 0;
            try
            {
                n = db.StockIsssueSequense.Max(c => c.Sequense);
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

            StockIssueSequense ls = new StockIssueSequense();
            ls.Sequense = n;
            db.StockIsssueSequense.Add(ls);
            db.SaveChanges();
            return n;
        }

        public String generateContractCode(int seq)
        {
            String s1 = "SI_";
            //String y = DateTime.Now.Year.ToString(); ;
            //s1 += y;
            // String S2 = " - CT";

            string S2 = seq.ToString();

            String s = s1 + S2;

            return s;
        }


        // GET: StockIssues/Create

        [Authorize(Roles = RoleNames.ROLE_StockIssuesCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            ViewBag.title1 = "Create Stock Issues";

            int Sequense = GetSeq();
            ViewBag.code = generateContractCode(Sequense);
            ViewBag.selectedDate = DateTime.Now;

            ViewBag.StockIssueTypeId = new SelectList(db.StockIssueTypes, "StockIssueTypeId", "Name");
            ViewBag.WarehouseId = new SelectList(db.Warehouses, "WarehouseId", "Name");
            ViewBag.LpoId  = new SelectList(db.Lpoes.Where(l=>l.StockIssues.Count() == 0), "LpoId", "code");

            ViewBag.CuttingSheetId = new SelectList(db.CuttingSheets, "CuttingSheetId", "code");
            return View();
        }

        // POST: StockIssues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockIssueId,StockIssueTypeId,WarehouseId,Code,Ref,UserCreate,IssueDate,Note")] StockIssue stockIssue)
        {
            if (ModelState.IsValid)
            {
                db.StockIssues.Add(stockIssue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockIssueTypeId = new SelectList(db.StockIssueTypes, "StockIssueTypeId", "Name", stockIssue.StockIssueTypeId);
            ViewBag.WarehouseId = new SelectList(db.Warehouses, "WarehouseId", "Name", stockIssue.WarehouseId);
            return View(stockIssue);
        }

        public JsonResult CreateJson([Bind(Include = "StockIssueId,StockIssueTypeId,WarehouseId,Code,Ref,UserCreate,IssueDate,Note,LpoId,CuttingSheetId")] StockIssue stockIssue)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(stockIssue.LpoId !=0 && stockIssue.LpoId != null)
                    {

                        var lpo = db.Lpoes.Find(stockIssue.LpoId);
                        lpo.LpoStatusId = 3;  //complete
                    }



                    stockIssue.StampDate = DateTime.Now;
                    stockIssue.UserCreate = User.Identity.GetUserName();
                    db.StockIssues.Add(stockIssue);
                    db.SaveChanges();
                    StockIssue s = db.StockIssues.Include(w => w.StockIssueType).Where(w => w.StockIssueId == stockIssue.StockIssueId).FirstOrDefault();

                    if (s.StockIssueType.Name == "Stock In (Puchase Invoice)")
                    {
                        this.notificationRepository.CreateNotificationAsync(stockIssue.StockIssueId, NotificationName.onStockIn);

                    }
                    else if (stockIssue.StockIssueType.Name == "Stock Out(Issue Material)")
                    {
                        this.notificationRepository.CreateNotificationAsync(stockIssue.StockIssueId, NotificationName.onStockOut);

                    }
                    return Json(stockIssue.StockIssueId);
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


            String errors111 = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors111 = errors111 + " <br /> " + error.ErrorMessage;
                }
            }
            return Json(errors111);
        }

        [HttpPost]
        public JsonResult CreateDetailsJson(List<StockIssueDetail> Details,int id)
        {
            string errors11 = "";
            int aaa = 0;
            int test = 0;
            int eid = 0;
            float oldQty = 0;

            StockIssue stockIssue = db.StockIssues.Find(id);


            StockIssueDetail EP = new StockIssueDetail();
            foreach (StockIssueDetail d in Details)
            {
                try
                {
                    EP = new StockIssueDetail();
                    EP.StockIssueId = int.Parse(d.StockIssueId.ToString());
                    eid = int.Parse(d.StockIssueId.ToString());

                    EP.MaterialId = int.Parse(d.MaterialId.ToString());
                    Material mm=  db.Materials.Find(EP.MaterialId);
                    EP.Price = float.Parse ( d.Price.ToString());


                    //calculat Qty
                    var StockIssue = db.StockIssues.Find(eid);
                    if (StockIssue.StockIssueType.Type == MoveType.InStore)
                    {
                        EP.InQty = float.Parse(d.InQty.ToString());
                        EP.OutQty = 0;

                        

                        //update material qty

                        oldQty = mm.qty??0;
                        
                        mm.qty = oldQty + EP.InQty;                    

                    }
                    else
                    {
                        EP.InQty = 0;
                        float OldResevedqty = mm.Resevedqty ?? 0;
                        EP.OutQty = float.Parse(d.OutQty.ToString());

                        //EP.BalanceQty = float.Parse(d.BalanceQty.ToString());

                        //update material qty
                        oldQty = mm.qty??0;
                        mm.qty = oldQty -  EP.OutQty;

                       
                        mm.Resevedqty = OldResevedqty - EP.OutQty;

                    }



                    //calculat Price

                    if (StockIssue.StockIssueType.Type == MoveType.InStore)
                    {
                        if (d.Price != null && d.Price != 0)
                        {
                            EP.Price = float.Parse(d.Price.ToString());

                            //update material avrgae price


                            mm.latestPrice = EP.Price;

                            float Avg = mm.AvgPrice ?? 0;
                            mm.AvgPrice = (Avg * oldQty + EP.Price * EP.InQty) / (mm.qty);
                        }
                    }

                    db.StockIssueDetails.Add(EP);



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


            if (stockIssue.CuttingSheetId != null)
            {
                CuttingSheet cuttingSheet = db.CuttingSheets.Find(stockIssue.CuttingSheetId);


                List<CuttingSheetDetail> cuttingSheetDetails = cuttingSheet.CuttingSheetDetails.ToList();

                foreach (CuttingSheetDetail cuttingSheetDetail in cuttingSheetDetails)
                {

                    float qty = stockIssue.StockIssueDetails.Where(s => s.MaterialId == cuttingSheetDetail.MaterialId).FirstOrDefault().OutQty;

                    cuttingSheetDetail.IssueQty += qty;

                    db.SaveChanges();
                }

            }


            if (test == 1)
            {
                return Json(errors11);
            }
            return Json("Cutting Sheet Saved Successfully");

        }

        // GET: StockIssues/Edit/5

        [Authorize(Roles = RoleNames.ROLE_StockIssuesEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockIssue stockIssue = db.StockIssues.Find(id);
            if (stockIssue == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockIssueTypeId = new SelectList(db.StockIssueTypes, "StockIssueTypeId", "Name", stockIssue.StockIssueTypeId);
            ViewBag.WarehouseId = new SelectList(db.Warehouses, "WarehouseId", "Name", stockIssue.WarehouseId);
            return View(stockIssue);
        }

        // POST: StockIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockIssueId,StockIssueTypeId,WarehouseId,Code,Ref,UserCreate,IssueDate,Note")] StockIssue stockIssue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockIssue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockIssueTypeId = new SelectList(db.StockIssueTypes, "StockIssueTypeId", "Name", stockIssue.StockIssueTypeId);
            ViewBag.WarehouseId = new SelectList(db.Warehouses, "WarehouseId", "Name", stockIssue.WarehouseId);
            return View(stockIssue);
        }

        // GET: StockIssues/Delete/5

        [Authorize(Roles =  RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockIssue stockIssue = db.StockIssues.Find(id);
            if (stockIssue == null)
            {
                return HttpNotFound();
            }
            return View(stockIssue);
        }

        // POST: StockIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {

           
            StockIssue stockIssue = db.StockIssues.Find(id);
            foreach(var x in stockIssue.StockIssueDetails.ToList())
            {
                db.StockIssueDetails.Remove(x);
            }

           db.StockIssues.Remove(stockIssue);
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
