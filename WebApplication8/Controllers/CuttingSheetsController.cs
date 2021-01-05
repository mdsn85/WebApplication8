using log4net;
using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Authorize]
    public class CuttingSheetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ILog log = LogManager.GetLogger("application-log");
        private INotificationRepository notificationRepository;


        public CuttingSheetsController()
        {
            this.notificationRepository = new NotificationRepository(new ApplicationDbContext());
        }
        // GET: CuttingSheets
        [Authorize(Roles = RoleNames.ROLE_MRFView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index(string SearchCode, string SearchProjectCode)
        {
            ViewBag.title1 = "MRF List";

            ViewBag.SearchCode = SearchCode;
            ViewBag.SearchProjectCode = SearchCode;
            

            List<CuttingSheet> cuttingSheets = db.CuttingSheets.Include(c => c.project).ToList();

            if (!String.IsNullOrEmpty(SearchCode))
            {
                cuttingSheets = cuttingSheets.Where(s => s.code.ToString().Contains(SearchCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }


            if (!String.IsNullOrEmpty(SearchProjectCode))
            {
                cuttingSheets = cuttingSheets.Where(s => s.project.Code.ToString().Contains(SearchProjectCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            cuttingSheets = cuttingSheets.OrderByDescending(p => p.StampDate).ToList();
            return View(cuttingSheets.ToList());
        }

        // GET: CuttingSheets/Details/5
        [Authorize(Roles = RoleNames.ROLE_MRFView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuttingSheet cuttingSheet = db.CuttingSheets.Find(id);

            ViewBag.AttachedFiles = cuttingSheet.CuttingSheetFiles.OrderBy(c => c.Name); ;
            if (cuttingSheet == null)
            {
                return HttpNotFound();
            }
            return View(cuttingSheet);
        }


        public JsonResult CuttingSheetsDetails(int CuttingSheetId)
        {
            var l = db.CuttingSheets.Find(CuttingSheetId);
            var ld = l.CuttingSheetDetails.Select(a => new
            {
                MaterialId = a.MaterialId,
                Name = a.material.Name,
                Unit = a.material.Unit.Name,
                Dimension = a.material.Dimension,

                Qty = a.qty,
                IssueQty = a.IssueQty,

                Price = a.material.AvgPrice,
                qtyApproved = a.qtyApproved,
                Approval = a.Approval,
                Remark = a.Remark,
                 CuttingSheetDetailId = a.CuttingSheetDetailId,
                type = a.material.MaterialType.Name

            }).ToList();
            return Json(ld, JsonRequestBehavior.AllowGet);
        }


        public int GetSeq()
        {
            int n = 0;
            try
            {
                n = db.CuttingSheetSequenses.Max(c => c.Sequense);
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

            CuttingSheetSequense ls = new CuttingSheetSequense();
            ls.Sequense = n;
            db.CuttingSheetSequenses.Add(ls);
            db.SaveChanges();
            return n;
        }

         public String generateContractCode(int seq)
        {
            String s1 = "MRHM_CS_";

            string S2 = seq.ToString();

            String s = s1 + S2;
            
            return s;
        }

        // GET: CuttingSheets/Create
        [Authorize(Roles = RoleNames.ROLE_MRFCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create(int? ProjectId)
        {
            ViewBag.title1 = "Create MRF ";
            int Sequense = GetSeq();
            ViewBag.code = generateContractCode(Sequense);

            var ProjectList = db.Projects.Where(p => p.AccountApproval == true && p.CuttingSheets.Count == 0)
                                .Select(p => new { ProjectId = p.ProjectId, Code = p.Code + " - " + p.Name });

            ViewBag.productlist = new SelectList(db.Materials, "MaterialId", "Name");
            ViewBag.ProjectId = new SelectList(ProjectList, "ProjectId", "Code", ProjectId);

            ViewBag.date = DateTime.Now;
            ViewBag.ext = FolderPath.allowedExtensions;
            return View();
        }



        // POST: CuttingSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_MRFCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create([Bind(Include = "CuttingSheetId,ProjectId,UserCreate,CreateDate,code")] CuttingSheet cuttingSheet,
            String[] FileName)
        {
            int savefiles = 0;
            if (ModelState.IsValid)
            {

                if (FileName != null && FileName.Count()>0)
                {
                    var proj = db.Projects.Find(cuttingSheet.ProjectId);
                    proj.ProjectStatusId = 4;
                    db.CuttingSheets.Add(cuttingSheet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Please upload Production Sheet";
                }

            }

            ViewBag.productlist = new SelectList(db.Materials, "MaterialId", "Name");

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", cuttingSheet.ProjectId);
            ViewBag.date = cuttingSheet.CreateDate;
            return View(cuttingSheet);
        }

        [Authorize(Roles = RoleNames.ROLE_MRFCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public JsonResult CreateJson([Bind(Include = "CuttingSheetId,ProjectId,UserCreate,CreateDate,code")] CuttingSheet cuttingSheet,
            String[] FileName)
        {
            int savefiles = 0;
            if (ModelState.IsValid)
            {
                if (FileName != null && FileName.Count() > 0)
                {
                    try
                    {
                        //change status of project
                        if (cuttingSheet.ProjectId != null)
                        {
                            var proj = db.Projects.Find(cuttingSheet.ProjectId);
                            proj.ProjectStatusId = 4;
                        }
                        cuttingSheet.StampDate = DateTime.Now;
                        cuttingSheet.UserCreate = User.Identity.GetUserName();
                        db.CuttingSheets.Add(cuttingSheet);
                        db.SaveChanges();

                        this.notificationRepository.CreateNotificationAsync(cuttingSheet.CuttingSheetId, NotificationName.onCreateMRF);


                        //file link
                        //save uploaded files record
                        CuttingSheetFile CuttingSheetFile = new CuttingSheetFile();
                        if (FileName != null)
                        {
                            foreach (string fn in FileName)
                            {
                                string st = "_CuttingSheet " + fn;
                                CuttingSheetFile.CuttingSheetId = cuttingSheet.CuttingSheetId;
                                //String c = db.Customers.Find(contract.CustomerId).CompanyName;

                                CuttingSheetFile.Name = fn;
                                CuttingSheetFile.Path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), st);



                                try
                                {
                                    db.CuttingSheetFiles.Add(CuttingSheetFile);
                                    db.SaveChanges();

                                    savefiles = 1;
                                }
                                catch (Exception e)
                                {

                                    ViewBag.Error += "Files not saved :" + e.Message;
                                    log.Error(" ERROR mylog - Error while save  file of project " + cuttingSheet.CuttingSheetId + ":" + e.Message + " , stacktrace:" + e.StackTrace);
                                }

                            }
                        }



                        return Json(cuttingSheet.CuttingSheetId);
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
                else
                {
                     string Error = "Please upload Production Sheet";
                    return Json(Error);
                }
            }
            return Json("errors11");
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ROLE_MRFCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public JsonResult CreateDetailsJson(List<CuttingSheetDetail> Details)
        {
            string errors11 = "";
            int aaa = 0;
            int test = 0;
            int eid = 0;
            CuttingSheetDetail EP = new CuttingSheetDetail();
            foreach (CuttingSheetDetail d in Details)
            {
                try
                {
                    EP = new CuttingSheetDetail();
                    EP.CuttingSheetId = int.Parse(d.CuttingSheetId.ToString());
                    eid = int.Parse(d.CuttingSheetId.ToString());
                    EP.MaterialId = int.Parse(d.MaterialId.ToString());

                    Material mm = db.Materials.Find(EP.MaterialId);

                    EP.qty = float.Parse(d.qty.ToString());


                    float? AvailableQty = mm.qty - mm.Resevedqty??0 - mm.MinReOrder??0;
                    if (AvailableQty> EP.qty) {
                        EP.status = statusList.InStock;
                    }else
                    {
                        EP.status = statusList.Purchase;
                    }
                    EP.IssueQty = 0;
                    db.CuttingSheetDetails.Add(EP);
                    db.SaveChanges();


      


                    var StockIssue = db.StockIssues.Find(eid);

                    mm.Resevedqty = mm.Resevedqty??0 + EP.qty;
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
        [Authorize(Roles = RoleNames.ROLE_MRFPrint + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult PrintNewTable(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CuttingSheet lpo = db.CuttingSheets.Find(id);

            //ViewBag.LpoTerms = db.LpoTerms.ToList();

            return View(lpo);

        }

        // GET: CuttingSheets/Edit/5
        [Authorize(Roles = RoleNames.ROLE_MRFEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuttingSheet cuttingSheet = db.CuttingSheets.Find(id);
            if (cuttingSheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", cuttingSheet.ProjectId);
            return View(cuttingSheet);
        }

        // POST: CuttingSheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CuttingSheetId,ProjectId,UserCreate,CreateDate")] CuttingSheet cuttingSheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuttingSheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Code", cuttingSheet.ProjectId);
            return View(cuttingSheet);
        }


        [Authorize(Roles = RoleNames.ROLE_MRFEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public JsonResult EditJson([Bind(Include = "CuttingSheetId,ProjectId,UserCreate,CreateDate,code")] CuttingSheet cuttingSheet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //cuttingSheet.UserCreate = User.Identity.GetUserName();

                    cuttingSheet.StampDate = DateTime.Now;

                    db.Entry(cuttingSheet).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(cuttingSheet.CuttingSheetId);
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
            return Json("errors11");
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ROLE_MRFEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public JsonResult EditDetailsJson(List<CuttingSheetDetail> Details,int id)
        {
            string errors11 = "";
            int aaa = 0;
            int test = 0;
            int eid = 0;

            //delete exist
            var cs = db.CuttingSheets.Find(id);
            List<CuttingSheetDetail> OldDetails = cs.CuttingSheetDetails.ToList();
            foreach (CuttingSheetDetail d in OldDetails)
            {
                db.CuttingSheetDetails.Remove(d);
                
            }
            db.SaveChanges();



            CuttingSheetDetail EP = new CuttingSheetDetail();
            foreach (CuttingSheetDetail d in Details)
            {
                try
                {
                    EP = new CuttingSheetDetail();
                    EP = d;
                    //EP.CuttingSheetId = int.Parse(d.CuttingSheetId.ToString());
                    eid = int.Parse(d.CuttingSheetId.ToString());
                    //EP.MaterialId = int.Parse(d.MaterialId.ToString());

                    Material mm = db.Materials.Find(EP.MaterialId);

                    //EP.qty = float.Parse(d.qty.ToString());


                    float? AvailableQty = mm.qty - mm.Resevedqty - mm.MinReOrder;
                    if (AvailableQty > EP.qty)
                    {
                        EP.status = statusList.InStock;
                    }
                    else
                    {
                        EP.status = statusList.Purchase;
                    }

                    db.CuttingSheetDetails.Add(EP);
                    db.SaveChanges();





                    var StockIssue = db.StockIssues.Find(eid);

                    mm.Resevedqty = mm.Resevedqty + EP.qty;
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




        // GET: CuttingSheets/Delete/5
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuttingSheet cuttingSheet = db.CuttingSheets.Find(id);
            if (cuttingSheet == null)
            {
                return HttpNotFound();
            }
            return View(cuttingSheet);
        }

        // POST: CuttingSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {
            CuttingSheet cuttingSheet = db.CuttingSheets.Find(id);

            foreach (var f in cuttingSheet.CuttingSheetFiles.ToList())
            {
                db.CuttingSheetFiles.Remove(f);
            }
            db.CuttingSheets.Remove(cuttingSheet);
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
