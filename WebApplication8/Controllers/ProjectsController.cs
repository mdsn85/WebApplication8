using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication8.Hubs;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {

        NotificationHub objNotifHub = new NotificationHub();
        ILog log = LogManager.GetLogger("application-log");



        private ApplicationDbContext db = new ApplicationDbContext();

        private INotificationRepository notificationRepository;
        public ProjectsController()
        {
            this.notificationRepository = new NotificationRepository(new ApplicationDbContext());
        }
        private string[] rolesArray;

        // GET: Projects
        [Authorize(Roles = RoleNames.ROLE_ProjectView + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Index(bool? NotApproved,string CustomerId, string st, string SearchCode, string StatusId,string SearchValue1,string SearchValue2)
        {
            ViewBag.title1 = "Project List";
            ViewBag.SearchCode = SearchCode;

            ViewBag.SearchValue1 = SearchValue1;
            ViewBag.SearchValue2 = SearchValue2;

            ViewBag.StatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "Name", StatusId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", CustomerId);

            int search = 0;

            List<Project> projects = db.Projects.Include(p => p.ProjectStatus).Include(p => p.SalesType).Include(p => p.CustomersType).Include(p => p.Customer).Include(p => p.Designer).Include(p => p.ProjectPaymentTerm).Include(p => p.SalesMan).ToList(); ;

            if (NotApproved == true)
            {
                projects = projects.Where(s => s.AccountApproval == false).ToList();
                search = 1;
            }
            
            if (!String.IsNullOrEmpty(SearchCode))
            {
                projects = projects.Where(s => s.Code.ToString().Contains(SearchCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
                search = 1;
            }

            if (!String.IsNullOrEmpty(StatusId))
            {
                int qqqq = int.Parse(StatusId);
                projects = projects.Where(s => s.ProjectStatusId != null && s.ProjectStatusId == qqqq).ToList();
                search = 1;
            }

            if (!String.IsNullOrEmpty(CustomerId))
            {
                int qqqq = int.Parse(CustomerId);
                projects = projects.Where(s => s.CustomerId != null && s.CustomerId == qqqq).ToList();
                search = 1;
            }



            if (!String.IsNullOrEmpty(SearchValue1))
            {
                float v1 = float.Parse(SearchValue1);
                
                if (!String.IsNullOrEmpty(SearchValue2))
                {
                    float v2 = float.Parse(SearchValue2);
                    projects = projects.Where(cb => cb.Value >= v1 && cb.Value < v2).ToList();
                }
                else
                {

                    projects = projects.Where(cb => cb.Value == v1).ToList();
                }
                search = 1;
            }


            if (search == 1)
            {
                projects = projects.OrderByDescending(p => p.StampDate).ToList();
                if (User.IsInRole(RoleNames.ROLE_FactoryCoordinator))
                {
                    projects = projects.Where(p => p.AccountApproval == true).ToList();
                }
            }else
            {
                projects = projects.Where(p=>p.CreateDate>DateTime.Now.AddMonths(-1)) .OrderByDescending(p => p.StampDate).ToList();
                if (User.IsInRole(RoleNames.ROLE_FactoryCoordinator))
                {
                    projects = projects.Where(p => p.AccountApproval == true).ToList();
                }
            }

            return View(projects);
        }


        [Authorize(Roles = RoleNames.ROLE_ProjectViewCustomize + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult IndexShort(bool? NotApproved, string CustomerId, string st, string SearchCode, string StatusId, string SearchValue1, string SearchValue2)
        {
            ViewBag.title1 = "Project List";
            ViewBag.SearchCode = SearchCode;

            ViewBag.SearchValue1 = SearchValue1;
            ViewBag.SearchValue2 = SearchValue2;

            ViewBag.StatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "Name", StatusId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", CustomerId);


            List<Project> projects = db.Projects.Include(p => p.ProjectStatus).Include(p => p.SalesType).Include(p => p.CustomersType).Include(p => p.Customer).Include(p => p.Designer).Include(p => p.ProjectPaymentTerm).Include(p => p.SalesMan)
                .ToList(); ;

            if (NotApproved == true)
            {
                projects = projects.Where(s => s.AccountApproval == false).ToList();
            }

            if (!String.IsNullOrEmpty(SearchCode))
            {
                projects = projects.Where(s => s.Code.ToString().Contains(SearchCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            if (!String.IsNullOrEmpty(StatusId))
            {
                int qqqq = int.Parse(StatusId);
                projects = projects.Where(s => s.ProjectStatusId != null && s.ProjectStatusId == qqqq).ToList();
            }

            if (!String.IsNullOrEmpty(CustomerId))
            {
                int qqqq = int.Parse(CustomerId);
                projects = projects.Where(s => s.CustomerId != null && s.CustomerId == qqqq).ToList();
            }



            if (!String.IsNullOrEmpty(SearchValue1))
            {
                float v1 = float.Parse(SearchValue1);

                if (!String.IsNullOrEmpty(SearchValue2))
                {
                    float v2 = float.Parse(SearchValue2);
                    projects = projects.Where(cb => cb.Value >= v1 && cb.Value < v2).ToList();
                }
                else
                {

                    projects = projects.Where(cb => cb.Value == v1).ToList();
                }
            }
            projects = projects.OrderByDescending(p => p.StampDate).ToList();
            if (User.IsInRole(RoleNames.ROLE_FactoryCoordinator))
            {
                projects = projects.Where(p => p.AccountApproval == true).ToList();
            }

            return View(projects);
        }


        //DetailsShort
        [Authorize(Roles = RoleNames.ROLE_ProjectView + "," + RoleNames.ROLE_ProjectViewCustomize + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult DetailsShort(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Project> projects = db.Projects.Include(p => p.ProjectStatus).Include(p => p.CustomersType).Include(p => p.SalesType).Include(p => p.Customer).Include(p => p.Designer).Include(p => p.ProjectPaymentTerm).Include(p => p.SalesMan).ToList(); ;
            Project project = db.Projects.Find(id);

            float totalPayment = 0;
            foreach (var payment in project.Payments)
            {
                totalPayment += payment.Amount;
            }

            ViewBag.totalPayment = totalPayment;

            ViewBag.AttachedFiles = project.ProjectFiles.OrderBy(c => c.Name); ;
            if (project == null)
            {
                return HttpNotFound();
            }


            return View(project);
        }



        // GET: Projects/Details/5
        [Authorize(Roles = RoleNames.ROLE_ProjectView + ","+ RoleNames.ROLE_ProjectViewCustomize +","+ RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Details(int? id,string msg)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Project> projects = db.Projects.Include(p => p.ProjectStatus).Include(p => p.CustomersType).Include(p => p.SalesType).Include(p => p.Customer).Include(p => p.Designer).Include(p => p.ProjectPaymentTerm).Include(p => p.SalesMan).ToList(); ;
            Project project = db.Projects.Find(id);

            float totalPayment = 0;
            foreach(var payment in project.Payments)
            {
                totalPayment += payment.Amount;
            }

            ViewBag.totalPayment = totalPayment;
            
            ViewBag.AttachedFiles = project.ProjectFiles.OrderBy(c => c.Name); ;
            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.Error = msg;
            if (User.IsInRole(RoleNames.ROLE_ProjectViewCustomize))
            {


                return RedirectToAction("DetailsShort", "Projects", new { id = project.ProjectId });
            }
            return View(project);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.ROLE_ProjectView + "," + RoleNames.ROLE_ADMINISTRATOR + "," + RoleNames.ROLE_Account)]
        public ActionResult Details(bool? AccountApprovalCk, int ProjectId)
        {




            Project proj = db.Projects
              .Include(i => i.ProjectPaymentTerm)
              .FirstOrDefault(x => x.ProjectId == ProjectId);

            string msg = "";
            if (AccountApprovalCk != null)
            {
                proj.AccountApproval = AccountApprovalCk ?? false;
            }

            if  ((proj.ProjectPaymentTerm.Name == "COD" ) )
            {

                db.SaveChanges();
                this.notificationRepository.CreateNotificationAsync(proj.ProjectId, NotificationName.onAccountApproval);

                //return RedirectToAction("Index");
            }
            else  
            if (proj.Payments != null && proj.Payments.Count() > 0)
            { 
                db.SaveChanges();
                this.notificationRepository.CreateNotificationAsync(proj.ProjectId, NotificationName.onAccountApproval);
            }
            else
            {
                msg= "You can not approve , no payment is made";
            }


            List<Project> projects = db.Projects.Include(p => p.ProjectStatus).Include(p => p.CustomersType).Include(p => p.SalesType).Include(p => p.Customer).Include(p => p.Designer).Include(p => p.ProjectPaymentTerm).Include(p => p.SalesMan).ToList(); ;
            Project project = db.Projects.Find(ProjectId);

            float totalPayment = 0;
            foreach (var payment in project.Payments)
            {
                totalPayment += payment.Amount;
            }

            ViewBag.totalPayment = totalPayment;

            ViewBag.AttachedFiles = project.ProjectFiles.OrderBy(c => c.Name); ;
            if (project == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Details", new { id = ProjectId ,msg=msg});
            
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
            String s1 = "Pro_";

            string S2 = seq.ToString();

            String s = s1 + S2;

            return s;
        }


        public ActionResult AddDeduction(int ProjectId, int? Close)
        {
            ViewBag.close = Close;
            ViewBag.ProjectId = ProjectId;


            Project proj = db.Projects.Find(ProjectId);
            ViewBag.a = proj.Deduction;
            ViewBag.r = proj.DeductionReason;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDeduction(int projectId , float deduction, string reason)
        {
            Project proj = db.Projects.Find(projectId);
            proj.Deduction = deduction;
            proj.DeductionReason = reason;
            db.SaveChanges();


            return RedirectToAction("AddDeduction", new { ProjectId = projectId, Close = 1 });
        }




        // GET: Projects/Create
        [Authorize(Roles = RoleNames.ROLE_ProjectCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create()
        {
            ViewBag.title1 = "Create Project ";
            int Sequense = GetSeq();
            ViewBag.code = generateContractCode(Sequense);

            ViewBag.SalesDate = DateTime.Now.ToString("dd-MMMM-yyyy", CultureInfo.InvariantCulture); 

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name");
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name");
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name");

            ViewBag.CustomersTypeId = new SelectList(db.CustomersTypes, "CustomersTypeId", "Name");

            ViewBag.SalesTypeId = new SelectList(db.SalesTypes, "SalesTypeId", "Name");

            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "Name");
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Name");
            

            ViewBag.ext = FolderPath.allowedExtensions;

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_ProjectCreate + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Create([Bind(Include = "Warranty,AreaId,ProjectId,Name,CustomersTypeId,SalesTypeId,Code,SalesDate,SalesManId,CustomerId,DesignerId,Value,Discount,Vat,Total,SalePrice,ProjectPaymentTermId" +
            ",Description,DeliveryDate,ActualDeliveryDate,AccountApproval,QuotationRef,QuotationAgreementApprovedby")] Project project,
            String[] FileName)
        {
            int saveProject = 0;
            int savefiles = 0;
       
            ViewBag.SalesDate = project.SalesDate.ToString("dd-MMMM-yyyy", CultureInfo.InvariantCulture); ;
            if (project.DeliveryDate != null)
            {
                ViewBag.DeliveryDate = project.DeliveryDate.ToString("dd-MMMM-yyyy", CultureInfo.InvariantCulture); ;
            }
            ViewBag.ActualDeliveryDate = project.ActualDeliveryDate;
            if (ModelState.IsValid)
            {
                if (FileName != null && FileName.Count()>0)
                {
                    try
                    {

                        project.Deduction = 0;
                        project.UserCreate = User.Identity.GetUserName();
                        project.CreateDate = DateTime.Now;
                        project.StampDate = DateTime.Now;
                        project.ProjectStatusId = 1;

                        db.Projects.Add(project);

                        try
                        {
                            db.SaveChanges();

                            saveProject = 1;
                        }
                        catch (Exception e)
                        {

                            ViewBag.Error += "Project not saved : " + e.Message ;
                            log.Error(" ERROR mylog - Error while save project to server:" + e.Message + " , stacktrace:" + e.StackTrace);
                        }


                        //file link
                        //save uploaded files record
                        ProjectFile projectFile = new ProjectFile();
                        if (FileName != null)
                        {
                            foreach (string fn in FileName)
                            {
                                string st = "_Project " + fn;
                                projectFile.ProjectId = project.ProjectId;
                                //String c = db.Customers.Find(contract.CustomerId).CompanyName;

                                projectFile.Name = fn;
                                projectFile.Path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), st);

                                

                                try
                                {
                                    db.projectFiles.Add(projectFile);
                                    db.SaveChanges();

                                    savefiles = 1;
                                }
                                catch (Exception e)
                                {

                                    ViewBag.Error += "Files not saved :" + e.Message;
                                    log.Error(" ERROR mylog - Error while save  file of project "+project.ProjectId+":" + e.Message + " , stacktrace:" + e.StackTrace);
                                }
                                
                            }
                        }

                        this.notificationRepository.CreateNotificationAsync(project.ProjectId, NotificationName.onCreateProject);

                        if (saveProject == 1)
                        {

                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(" ERROR mylog - Error while upload file to server:" + e.Message + " , stacktrace:" + e.StackTrace);

                       
                    }
                }
                else
                {
                    ViewBag.Error = "Please upload Production Sheet";
                }
            }
            ViewBag.title1 = "Create Project ";
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", project.CustomerId);
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name", project.DesignerId);
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name", project.ProjectPaymentTermId);
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name", project.SalesManId);



            ViewBag.CustomersTypeId = new SelectList(db.CustomersTypes, "CustomersTypeId", "Name", project.CustomersTypeId);

            ViewBag.SalesTypeId = new SelectList(db.SalesTypes, "SalesTypeId", "Name", project.SalesTypeId);
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Name",project.AreaId);
            ViewBag.code = project.Code;
            ViewBag.ext = FolderPath.allowedExtensions;



            ViewBag.DeliveryDate = project.DeliveryDate;
            ViewBag.Warranty= project.Warranty;
            ViewBag.ActualDeliveryDate = project.ActualDeliveryDate;


            return View(project);
        }


        //async Task CreateNotificationAsync(Project project, string category)
        //{

        //    try
        //    {
        //        Notification1 objNotif = new Notification1();
        //        objNotif.NotificationCategoryId = db.NotificationCategorys.Where(c => c.Name == NotificationName.onCreateProject).FirstOrDefault().NotificationCategoryId;

        //        objNotif.ObjectId = project.ProjectId;
        //        objNotif.NotificationDate = DateTime.Now;

        //        db.Configuration.ProxyCreationEnabled = false;
        //        db.Notifications.Add(objNotif);
        //        db.SaveChanges();
        //        SendNoficationAsync(objNotif);

        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(" ERROR mylog - Error while send notification for project " + project.ProjectId + ":" + e.Message + " , stacktrace:" + e.StackTrace);
        //    }

        //}

        //async  Task SendNoficationAsync(Notification1 objNotif)
        //{
        //    string[] SentTo = objNotif.NotificationCategory.NotificationCatUser.Select(n => n.UserId).ToArray();
        //    objNotifHub.SendNotification(SentTo);
        //}


        // GET: Projects/Edit/5
        [Authorize(Roles = RoleNames.ROLE_ProjectEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit(int? id)
        {
 

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentTermId1 = project.ProjectPaymentTermId;
            ViewBag.PaymentTermName1 = db.ProjectPaymentTerms.Find(project.ProjectPaymentTermId).Name;

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", project.CustomerId);
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name", project.DesignerId);
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name", project.ProjectPaymentTermId);
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name", project.SalesManId);

            ViewBag.CustomersTypeId = new SelectList(db.CustomersTypes, "CustomersTypeId", "Name", project.CustomersTypeId);

            ViewBag.SalesTypeId = new SelectList(db.SalesTypes, "SalesTypeId", "Name", project.SalesTypeId);

            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "Name", project.ProjectStatusId);
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Name", project.AreaId);
            project.ProjectFiles = db.Projects.Find(project.ProjectId).ProjectFiles;

            ViewBag.ext = FolderPath.allowedExtensions;
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.ROLE_ProjectEdit + "," + RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Edit([Bind(Include = "Warranty,AreaId,ProjectId,UserCreate,CreateDate,Deduction,ProjectStatusId,DeductionReason,Name,CustomersTypeId,SalesTypeId,Code,SalesDate,SalesManId,CustomerId,DesignerId,Value," +
            "Discount,Vat,Total,SalePrice,ProjectPaymentTermId,Description,DeliveryDate,ActualDeliveryDate,AccountApproval,QuotationRef,QuotationAgreementApprovedby")] Project project
            , String[] FileName)
        {
            if (ModelState.IsValid)
            {
                project.UserLastUpdate = User.Identity.GetUserName();
                project.LastUpdateDate = DateTime.Now;

                project.StampDate = DateTime.Now;

                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                RemoveFiles(project.ProjectId);
                //save uploaded files record
                if (FileName.Count() > 0)
                {
                    SaveAttachedFiles(FileName, project.ProjectId);
                }

                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", project.CustomerId);
            ViewBag.DesignerId = new SelectList(db.Designers, "DesignerId", "Name", project.DesignerId);
            ViewBag.ProjectPaymentTermId = new SelectList(db.ProjectPaymentTerms, "ProjectPaymentTermId", "Name", project.ProjectPaymentTermId);
            ViewBag.SalesManId = new SelectList(db.SalesMen, "SalesManId", "Name", project.SalesManId);

            ViewBag.CustomersTypeId = new SelectList(db.CustomersTypes, "CustomersTypeId", "Name", project.CustomersTypeId);

            ViewBag.SalesTypeId = new SelectList(db.SalesTypes, "SalesTypeId", "Name", project.SalesTypeId);

            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "ProjectStatusId", "Name", project.ProjectStatusId);
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Name", project.AreaId);
            return View(project);
        }

        private void RemoveFiles(int projectId)
        {
            Project project = db.Projects.Include(c => c.ProjectFiles).Where(c => c.ProjectId == projectId).FirstOrDefault();
            if (project.ProjectFiles.Count() > 0)
            {
                foreach (var file in project.ProjectFiles.ToList())
                {
                    ProjectFile projectFile = db.projectFiles.Find(file.ProjectFileId);
                    db.projectFiles.Remove(projectFile);
                }
                db.SaveChanges();
            }
        }

        private int SaveAttachedFiles(string[] FileNames, int projectId)
        {
            int savefiles = 0;
            ProjectFile projectFile = new ProjectFile();
            if (FileNames != null)
            {
                foreach (string FileName in FileNames)
                {
                    string st = "_CuttingSheet " + FileName;
                    projectFile.ProjectId = projectId;
                    projectFile.Name = FileName;
                    projectFile.Path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), st);
                    try
                    {
                        db.projectFiles.Add(projectFile);
                        db.SaveChanges();
                        savefiles = 1;
                    }
                    catch (Exception e)
                    {
                        ViewBag.Error += "Files not saved :" + e.Message;
                        log.Error(" ERROR mylog - Error while save  file of project " + projectId + ":" + e.Message + " , stacktrace:" + e.StackTrace);
                    }
                }
            }
            return savefiles;
        }


        // GET: Projects/Edit/5
        public ActionResult AccountApproval(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Project> projects = db.Projects.Include(p => p.CustomersType).Include(p => p.SalesType).Include(p => p.Customer).Include(p => p.Designer).Include(p => p.ProjectPaymentTerm).Include(p => p.SalesMan).ToList(); ;
            Project project = db.Projects.Find(id);
            ViewBag.AttachedFiles = project.ProjectFiles.OrderBy(c => c.Name); ;
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult AccountApproval(bool? AccountApprovalCk, int ProjectId)
        {


                Project proj = db.Projects.Find(ProjectId);

            if (AccountApprovalCk != null)
            {
                proj.AccountApproval = AccountApprovalCk ?? false;
            }

            if (proj.Payments != null && proj.Payments.Count() > 0)
            {

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "You can not approve , no payment is made";
            }

            
            return View(proj);
        }


        public string FileUploadForEdit(HttpPostedFileBase file)
        {

            //upload file check extention
            var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", ".jpeg",".pdf",".doc",".docx"
                };
            allowedExtensions = FolderPath.allowedExtensions;
            //*****************

            String FileName = "";
            if (file != null)
            {
                //save file to server
                //var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                //string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                ext = ext.ToLower();
                string OrginalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                if (!allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    ViewBag.message = "Please choose only Image or PDF file";

                    //int Id = session.LegalCaseId;
                    //ViewBag.LegalCaseId = new SelectList(db.LegalCases.Where(l => l.LegalCaseId == Id), "LegalCaseId", "OfficeNum");
                    ////اخر جلسة مرحلة
                    //ViewBag.leatestTransSession = db.LegalCases.Where(l => l.LegalCaseId == Id).FirstOrDefault().leatestTransSession;
                    ////اخر جلسة غير مرحلة اي الجلسة القادمة
                    //ViewBag.NextSession = db.LegalCases.Where(l => l.LegalCaseId == Id).FirstOrDefault().NextSession;
                    //ViewBag.LegalCaseId = new SelectList(db.LegalCases, "LegalCaseId", "Subject", session.LegalCaseId);
                    return "Please choose only Image or PDF file";
                }

                string RandomFileName = Path.GetRandomFileName();
                RandomFileName = RandomFileName.Substring(0, RandomFileName.Length - 4);
                FileName = OrginalFileName + DateTime.Now.ToString("dd MMMM yyyy") + ext;
                var path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), FileName);
                file.SaveAs(path);
                //************************

                ////save file record
                //sessionDecisionFiles.SessionDecisionId = sessionDecisionFiles.SessionDecisionId;
                //sessionDecisionFiles.Name = FileName;
                //sessionDecisionFiles.Path = path;

                //db.SessionDecisionFiles.Add(sessionDecisionFiles);
            }

            //  Send "Success"
            //return "done";

            // return "{\"msg\":\"success\"}";ReportNum

            return "{\"msg\":\"" + FileName + "\"}";
            // return "done";
        }

        // GET: Projects/Delete/5
        [Authorize(Roles =  RoleNames.ROLE_ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

           
            Project project = db.Projects.Find(id);

            foreach(var f in project.ProjectFiles.ToList())
            {
                db.projectFiles.Remove(f);
            }
            db.Projects.Remove(project);
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