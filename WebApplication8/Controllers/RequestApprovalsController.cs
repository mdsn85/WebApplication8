using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class RequestApprovalsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestApprovals
        public ActionResult Index(bool? searchApprovedOnly, bool? searchRejectedOnly, bool? searchall, string searchStringCustomerName)
        {
            //by defult not approved request
            List<RequestApproval> requestApprovals = db.RequestApprovals.Where(ra => ra.CEO != true && ra.CoordinatorFull != true && ra.IsRejected != true).Include(r => r.CuttingSheet).Include(r => r.Quotation).Include(r => r.ApprovalType).ToList();


            if (searchApprovedOnly == true)
            {
                requestApprovals = db.RequestApprovals.Where(ra => ra.CEO == true || ra.CoordinatorFull == true).Include(r => r.CuttingSheet).Include(r => r.Quotation).Include(r => r.ApprovalType).ToList();
            }
            if (searchRejectedOnly == true)
            {
                requestApprovals = db.RequestApprovals.Where(ra => ra.IsRejected == true).Include(r => r.CuttingSheet).Include(r => r.Quotation).Include(r => r.ApprovalType).ToList();
            }


            if (searchall == true)
            {
                requestApprovals = db.RequestApprovals.Include(r => r.CuttingSheet).Include(r => r.ApprovalType).ToList();
            }


          

            requestApprovals = requestApprovals.OrderByDescending(ra => ra.RequestDate).ToList();
            return View(requestApprovals.ToList());
        }
        [HttpPost]

        public int sendEmail(String Subject, String emailBody, List<string> TO, List<string> CC)
        {
            using (MailMessage mail = new MailMessage())
            {
                try
                {
                    mail.From = new MailAddress(EmailSetting.MailFrom);

                    if (TO != null)
                    {
                        foreach (string to in TO)
                        {
                            MailAddress copy = new MailAddress(to);
                            mail.To.Add(copy);
                        }
                    }

                    if (CC != null)
                    {

                        foreach (string cc in CC)
                        {
                            MailAddress copy = new MailAddress(cc);
                            mail.CC.Add(copy);
                        }
                    }

                    mail.Subject = Subject;
                    mail.Body = emailBody;


                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    EmailServer es = db.EmailServers.FirstOrDefault();
                    smtp.Host = es.Host;
                    smtp.EnableSsl = es.EnableSsl; ;
                    NetworkCredential networkCredential = new NetworkCredential(EmailSetting.MailFrom, EmailSetting.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = es.Port;
                    smtp.Send(mail);

                    ViewBag.Message = "Email Sent ";
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Fail to send Email \r\n " + e.Message; ;
                    return 0;
                }
                return 1;
            }



        }

        private async void sendEmail1(String Subject, String emailBody, String[] emailList)
        {
            SmtpClient smtpClient = new SmtpClient(EmailSetting.Host, EmailSetting.Port);

            smtpClient.Credentials = new System.Net.NetworkCredential(EmailSetting.Credintial, EmailSetting.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(EmailSetting.MailFrom);
            mailMessage.Subject = Subject;
            mailMessage.Body = emailBody;

            foreach (String item in emailList)
            {
                mailMessage.To.Add(item);
            }

            smtpClient.Send(mailMessage);
        }

        // GET: RequestApprovals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestApproval requestApproval = db.RequestApprovals.Find(id);
            if (requestApproval == null)
            {
                return HttpNotFound();
            }
            return View(requestApproval);
        }

        // GET: RequestApprovals/Create
        public ActionResult Create()
        {
            ViewBag.RequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ViewBag.CuttingSheetId = new SelectList(db.CuttingSheets, "CuttingSheetId", "Code");
            ViewBag.QuotationId = new SelectList(db.Quotations, "QuotationId", "Code");
            ViewBag.ApprovalTypeId = new SelectList(db.ApprovalTypes, "ApprovalTypeId", "Name");
            return View();
        }

        // POST: RequestApprovals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestApprovalId,Name,RequestDate,ApprovalTypeId,QuotationId,CuttingSheetId,details,UserName,SalesManager,CoordinatorFull,CoordinatorPartial,CEO")] RequestApproval requestApproval)
        {
            if (ModelState.IsValid)
            {
                requestApproval.RequestDate = DateTime.Now;
                requestApproval.UserName = User.Identity.GetUserName();

                try
                {
                    int PermissionId = db.PermissionUsers.Where(pu => pu.Username == requestApproval.UserName).FirstOrDefault().PermissionId;
                    string PermisssionName = db.Permissions.Find(PermissionId).Name;
                    if (PermisssionName == PermissionNames.Permission_BDM)
                    {
                        requestApproval.BDM = true;
                    }

                    if (PermisssionName == PermissionNames.Permission_SalesManager)
                    {
                        requestApproval.SalesManager = true;
                    }


                }
                catch (Exception e) { }



                db.RequestApprovals.Add(requestApproval);
                db.SaveChanges();

                List<string> emails = new List<string>();
                List<string> CC = new List<string>();
                string SubjectForCase = "";

                string emailBodys = "";
                if (requestApproval.QuotationId != null)
                {
                    if (User.IsInRole(RoleNames.ROLE_Coordinator))
                    {
                        emails.Add("alveera@mrskips.net");
                        int qid = requestApproval.QuotationId ?? 0;
                        int SalesManId = db.Quotations.Find(qid).SalesManId;
                        try
                        {
                            int SalesTeamId = db.SalesMen.Find(SalesManId).SalesTeams.SalesTeamId;
                            int ReportToId = db.SalesTeam.Find(SalesTeamId).EmployeeId ?? 0;
                            Employee ReportTo = db.Employees.Find(ReportToId);
                            string ReportToUsername = db.EmployeeUsers.Where(e => e.EmployeeId == ReportToId).FirstOrDefault().User;
                            string ReportToEmail = db.Users.Where(u => u.UserName == ReportToUsername).FirstOrDefault().Email;
                            CC.Add(ReportToEmail);
                        }
                        catch (Exception e) { }
                    }

                    //if (User.IsInRole(RoleNames.ROLE_Sales))
                    //{
                    //    int SalesManId1 = requestApproval.CuttingSheet.SalesManId;

                    //    SalesTeam SalesTeam = db.SalesMan.Find(SalesManId1).SalesTeams;
                    //    if (SalesTeam.Name == "BDM 1" || SalesTeam.Name == "BDM 2")
                    //    {
                    //        emails.Add("alveera@mrskips.net");
                    //    }
                    //    if (SalesTeam.Name == "Sales Team 1")
                    //    {

                    //        int ReportToId1 = db.SalesTeam.Find(SalesManId1).EmployeeId ?? 0;
                    //        Employee ReportTo1 = db.employees.Find(ReportToId1);
                    //        string ReportToUsername1 = db.EmployeeUsers.Where(e => e.EmployeeId == ReportToId1).FirstOrDefault().User;
                    //        string ReportToEmail1 = db.Users.Where(u => u.UserName == ReportToUsername1).FirstOrDefault().Email;
                    //        emails.Add(ReportToEmail1);
                    //    }
                    //}
                    SubjectForCase = "QUOTATION REVISED - APPROVAL REQUIRED TO PROCEED WITH THE CHANGES";

                    Quotation q = db.Quotations.Find(requestApproval.QuotationId);
                   // emailBodys = "Dear Team, \r\n  Your approval is required for " + q.CustomerName + "  Quotation : " + q.Code + " \r\n ";
                    emailBodys = emailBodys + " The change requested is " + requestApproval.details;

                    //CC.Add("azmina@mrskips.net");
                    //get manager of sales

                    //CC.Add("mdsn85@gmail.com");

                }
                else
                {
                    if (requestApproval.CuttingSheetId != null)
                    {
                        CuttingSheet c = db.CuttingSheets.Find(requestApproval.CuttingSheetId);
                        emails.Add("alveera@mrskips.net");
                        // CC.Add("azmina@mrskips.net");
                        //get manager of sales



                        //int SalesManId = requestApproval.CuttingSheet.SalesManId;

                        //try
                        //{
                        //    int SalesTeamId = db.SalesMan.Find(SalesManId).SalesTeams.SalesTeamId;
                        //    int ReportToId = db.SalesTeam.Find(SalesTeamId).EmployeeId ?? 0;
                        //    Employee ReportTo = db.employees.Find(ReportToId);
                        //    string ReportToUsername = db.EmployeeUsers.Where(e => e.EmployeeId == ReportToId).FirstOrDefault().User;
                        //    string ReportToEmail = db.Users.Where(u => u.UserName == ReportToUsername).FirstOrDefault().Email;
                        //    CC.Add(ReportToEmail);
                        //}

                        //catch (Exception e) { }
                        // CC.Add("azmina@mrskips.net");
                        SubjectForCase = "CuttingSheet REVISED - APPROVAL REQUIRED TO PROCEED WITH THE CHANGES";
                        //emailBodys = "Dear Team, \r\n  Your approval is required for " + c.Customer.CompanyName + "  CuttingSheet : " + c.Code + " \r\n ";
                        emailBodys = emailBodys + " The change requested is " + requestApproval.details;
                    }
                }

                //int i = sendEmail(SubjectForCase, emailBodys, emails, CC);
                //sendEmail1(SubjectForCase, emailBodys,emails);
                return RedirectToAction("Index");
            }


            ViewBag.CuttingSheetId = new SelectList(db.CuttingSheets, "CuttingSheetId", "Code", requestApproval.CuttingSheetId);
            ViewBag.QuotationId = new SelectList(db.Quotations, "QuotationId", "CustomerName", requestApproval.QuotationId);
            ViewBag.ApprovalTypeId = new SelectList(db.ApprovalTypes, "ApprovalTypeId", "Name", requestApproval.ApprovalTypeId);
            return View(requestApproval);
        }



        public JsonResult CreateJson([Bind(Include = "RequestApprovalId,Name,RequestDate,ApprovalTypeId,QuotationId,CuttingSheetId,details,UserName,SalesManager,CoordinatorFull,CoordinatorPartial,CEO")] RequestApproval requestApproval)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    requestApproval.RequestDate = DateTime.Now;
                    requestApproval.UserName = User.Identity.GetUserName();

                    try
                    {
                        int PermissionId = db.PermissionUsers.Where(pu => pu.Username == requestApproval.UserName).FirstOrDefault().PermissionId;
                        string PermisssionName = db.Permissions.Find(PermissionId).Name;


                        if (PermisssionName == PermissionNames.Permission_ProductionManager)
                        {
                            requestApproval.ProductionManager = true;
                        }

                    }
                    catch (Exception e) { }



                    db.RequestApprovals.Add(requestApproval);
                    db.SaveChanges();

                    List<string> emails = new List<string>();
                    List<string> CC = new List<string>();
                    string SubjectForCase = "";

                    string emailBodys = "";

                    if (requestApproval.CuttingSheetId != null)
                    {
                        //get production manager email

                        string Username = db.PermissionUsers.Where(pu => pu.Permissions.Name == PermissionNames.Permission_ProductionManager).FirstOrDefault().Username;
                        string email = db.Users.Where (u=>u.UserName == Username).FirstOrDefault().Email;
                        emails.Add(email);
                        emails.Add("mdsn85@gmail.com");
                        CuttingSheet c = db.CuttingSheets.Find(requestApproval.CuttingSheetId);

                        SubjectForCase = "Extra Issue Material Requested - APPROVAL REQUIRED TO PROCEED WITH THE CHANGES";
                        emailBodys = "Dear Team, \r\n  Your approval is required for CuttingSheet : " + c.code + " \r\n ";
                        emailBodys = emailBodys + " The change requested is " + requestApproval.details;

                        int i = sendEmail(SubjectForCase, emailBodys, emails, CC);
                        //sendEmail1(SubjectForCase, emailBodys,emails);
                    }


                    return Json(requestApproval.CuttingSheetId);
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
        public JsonResult CreateDetailsJson(List<CuttingSheetDetail> Details)
        {
            string errors11 = "";
            int aaa = 0;
            int test = 0;
            int eid = 0;
            CuttingSheetDetail EP = new CuttingSheetDetail();


            foreach (var d in Details)
            {
                CuttingSheetDetail csd = db.CuttingSheetDetails.Find(d.CuttingSheetDetailId);
                csd.qtyApproved = d.qtyApproved;
                csd.Remark = d.Remark;
                csd.Approval = false;
                db.SaveChanges();
                

            }
            return Json("Cutting Sheet Saved Successfully");


        }
        // GET: RequestApprovals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RequestApproval requestApproval = db.RequestApprovals.Find(id);


            if (requestApproval.CoordinatorFull == true
                || requestApproval.CEO == true)
            {
                string message = "Sorry ,  You do not have permission to Edit. <br/> " +
                    "It is already approve. <br/> " +
                    "Contact management ";
                return ErrorPermission(message);
            }
            if (requestApproval == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuttingSheetId = new SelectList(db.CuttingSheets, "CuttingSheetId", "Code", requestApproval.CuttingSheetId);
            ViewBag.QuotationId = new SelectList(db.Quotations, "QuotationId", "Code", requestApproval.QuotationId);
            ViewBag.ApprovalTypeId = new SelectList(db.ApprovalTypes, "ApprovalTypeId", "Name", requestApproval.ApprovalTypeId);
            return View(requestApproval);
        }

        // POST: RequestApprovals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestApprovalId,Name,RequestDate,ApprovalTypeId,QuotationId,CuttingSheetId,details,UserName,SalesManager,CoordinatorFull,CoordinatorPartial,CEO")] RequestApproval requestApproval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestApproval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuttingSheetId = new SelectList(db.CuttingSheets, "CuttingSheetId", "Code", requestApproval.CuttingSheetId);
            ViewBag.QuotationId = new SelectList(db.Quotations, "QuotationId", "CustomerName", requestApproval.QuotationId);
            ViewBag.ApprovalTypeId = new SelectList(db.ApprovalTypes, "ApprovalTypeId", "Name", requestApproval.ApprovalTypeId);
            return View(requestApproval);
        }


        // GET: RequestApprovals/Edit/5
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestApproval requestApproval = db.RequestApprovals.Find(id);
            if (requestApproval == null)
            {
                return HttpNotFound();
            }

            int empid = db.EmployeeUsers.Where(eu => eu.User == requestApproval.UserName).FirstOrDefault().EmployeeId;
            ViewBag.employee = db.Employees.Find(empid).Name;

            ViewBag.CuttingSheetId = new SelectList(db.CuttingSheets, "CuttingSheetId", "Code", requestApproval.CuttingSheetId);
            ViewBag.QuotationId = new SelectList(db.Quotations, "QuotationId", "CustomerName", requestApproval.QuotationId);
            ViewBag.ApprovalTypeId = new SelectList(db.ApprovalTypes, "ApprovalTypeId", "Name", requestApproval.ApprovalTypeId);
            String username = User.Identity.GetUserName();
            try
            {
                List<int> a = db.PermissionUsers.Where(pu => pu.Username == username).Select(pu => pu.PermissionId).ToList();
                ViewBag.ListOfpermistion = db.Permissions.Where(p => a.Contains(p.PermissionId)).Select(p => p.Name).ToList();
            }
            catch (Exception e) { }
            return View(requestApproval);
        }

        // POST: RequestApprovals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([Bind(Include = "RequestApprovalId,Name,RequestDate,ApprovalTypeId,QuotationId,CuttingSheetId,details,UserName" +
            ",SalesManager,BDM" +
            ",CoordinatorFull,CoordinatorPartial,CEO,SalesManagerRemarks ,BDMRemarks ,CoordinatorFullRemarks ,CoordinatorPartialRemarks ,CEORemarks ,IsRejected,CoordinatorFullDate" +
            ",SalesManagerDate,CoordinatorPartialDate,CEODate,BDMDate")] RequestApproval requestApproval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestApproval).State = EntityState.Modified;
                db.SaveChanges();

                //remove rejection after aprove
                if (requestApproval.CoordinatorFull || requestApproval.CEO)
                {
                    requestApproval.IsRejected = false;
                }
                //CuttingSheet : ReOpen for Edit
                if (requestApproval.ApprovalTypeId == 1 || requestApproval.ApprovalTypeId == 3)
                {
                    if (requestApproval.SalesManager == true && requestApproval.CoordinatorFull == true)
                    {
                        CuttingSheet c = db.CuttingSheets.Find(requestApproval.CuttingSheetId);
                       // c.isLock = false;
                        db.SaveChanges();
                    }

                    if (requestApproval.CoordinatorFull == true)
                    {
                        CuttingSheet c = db.CuttingSheets.Find(requestApproval.CuttingSheetId);
                      //  c.isLock = false;
                        db.SaveChanges();
                    }

                    if (requestApproval.SalesManager == true && requestApproval.CoordinatorPartial == true && requestApproval.CEO == true)
                    {
                        CuttingSheet c = db.CuttingSheets.Find(requestApproval.CuttingSheetId);
                      //  c.isLock = false;
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (requestApproval.SalesManager == true && requestApproval.CoordinatorFull == true)
                    {
                        Quotation c = db.Quotations.Find(requestApproval.QuotationId);
                     //   c.isLock = false;
                        db.SaveChanges();
                    }

                    if (requestApproval.CoordinatorFull == true)
                    {
                        Quotation c = db.Quotations.Find(requestApproval.QuotationId);
                      //  c.isLock = false;
                        db.SaveChanges();
                    }

                    if (requestApproval.SalesManager == true && requestApproval.CoordinatorPartial == true && requestApproval.CEO == true)
                    {
                        Quotation c = db.Quotations.Find(requestApproval.QuotationId);
                     //   c.isLock = false;
                        db.SaveChanges();
                    }

                }


                // send emails
                List<string> emails = new List<string>();
                List<string> CC = new List<string>();
                string SubjectForCase = "";
                string emailBodys = "";
                if (requestApproval.QuotationId != null)
                {
                    SubjectForCase = "Request for Quotation Approval is Responded";
                    Quotation q = db.Quotations.Find(requestApproval.QuotationId);
                    emailBodys = "Dear Team, \r\n  Your request for approval  is responded with the remark " + requestApproval.CoordinatorFullRemarks + "  Quotation : " + q.Code + " is created \r\n ";
                   //emailBodys = emailBodys + " company name: " + requestApproval.Quotation.CustomerName + ". for your request details " + requestApproval.details;
                }
                else if (requestApproval.CuttingSheetId != null)
                {
                    SubjectForCase = "Request for CuttingSheet Approval is Responded";

                    CuttingSheet c = db.CuttingSheets.Find(requestApproval.CuttingSheetId);
                    emailBodys = "Dear Team, \r\n Your request for approval  is responded with the remark " + requestApproval.CoordinatorFullRemarks + "  CuttingSheet : " + c.code + " \r\n ";
                   // emailBodys = emailBodys + " company name:" + requestApproval.CuttingSheet.Customer.CompanyName + ". for your request details " + requestApproval.details;

                    if (requestApproval.ApprovalTypeId == 3)
                    {
                        CC.Add("sales@mrskips.net");
                    }
                }

                string username = requestApproval.UserName;
                string emailCreate = db.Users.Where(r => r.UserName == username).FirstOrDefault().Email;
                //send to sender
                if (requestApproval.CoordinatorFull == true)
                {
                    emails.Add(emailCreate);
                }
                //send to sender
                if (requestApproval.CEO == true)
                {
                    emails.Add(emailCreate);
                }
                if ((requestApproval.CoordinatorFull == false && requestApproval.CoordinatorPartial == false)
                    || requestApproval.SalesManager == true || requestApproval.BDM == true)
                {
                    emails.Add("alveera@mrskips.net");
                }
                if (requestApproval.CoordinatorPartial == true && requestApproval.CEO == false)
                {
                    // emails.Add("alveera@mrskips.net");
                }


                int i = sendEmail(SubjectForCase, emailBodys, emails, CC);

                return RedirectToAction("Index");
            }
            ViewBag.CuttingSheetId = new SelectList(db.CuttingSheets, "CuttingSheetId", "Code", requestApproval.CuttingSheetId);
            ViewBag.QuotationId = new SelectList(db.Quotations, "QuotationId", "CustomerName", requestApproval.QuotationId);
            ViewBag.ApprovalTypeId = new SelectList(db.ApprovalTypes, "ApprovalTypeId", "Name", requestApproval.ApprovalTypeId);
            return View(requestApproval);
        }





        

        public JsonResult ApproveJson([Bind(Include = "RequestApprovalId,Name,RequestDate,ApprovalTypeId,QuotationId,CuttingSheetId,details,UserName" +
            ",SalesManager,BDM,ProductionManager" +
            ",CoordinatorFull,CoordinatorPartial,CEO,SalesManagerRemarks ,ProductionManagerRemarks,BDMRemarks ,CoordinatorFullRemarks ,CoordinatorPartialRemarks ,CEORemarks ,IsRejected,CoordinatorFullDate" +
            ",SalesManagerDate,ProductionManagerDate,CoordinatorPartialDate,CEODate,BDMDate")] RequestApproval requestApproval)
        {

            requestApproval.IsRejected = false;
            //if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(requestApproval).State = EntityState.Modified;
                    db.SaveChanges();
                }catch(Exception er)
                {
                    string errors11 = er.Message;
                    return Json(errors11);
                }

               // send emails
                List<string> emails = new List<string>();
                List<string> CC = new List<string>();
                string SubjectForCase = "";
                string emailBodys = "";
                if (requestApproval.QuotationId != null)
                {
                    SubjectForCase = "Request for Quotation Approval is Responded";
                    Quotation q = db.Quotations.Find(requestApproval.QuotationId);
                    emailBodys = "Dear Team, \r\n  Your request for approval  is responded with the remark " + requestApproval.CoordinatorFullRemarks + "  Quotation : " + q.Code + " is created \r\n ";
                    //emailBodys = emailBodys + " company name: " + requestApproval.Quotation.CustomerName + ". for your request details " + requestApproval.details;
                }
                else if (requestApproval.CuttingSheetId != null)
                {
                    SubjectForCase = "Request for CuttingSheet Approval is Responded";

                    CuttingSheet c = db.CuttingSheets.Find(requestApproval.CuttingSheetId);
                    emailBodys = "Dear Team, \r\n Your request for approval  is responded with the remark " + requestApproval.ProductionManagerRemarks + "  CuttingSheet : " + c.code + " \r\n ";
                    // emailBodys = emailBodys + " company name:" + requestApproval.CuttingSheet.Customer.CompanyName + ". for your request details " + requestApproval.details;


                }

                string username = requestApproval.UserName;
                string emailCreate = db.Users.Where(r => r.UserName == username).FirstOrDefault().Email;
                //send to sender
                if (requestApproval.ProductionManager == true)
                {
                    emails.Add(emailCreate);
                }
               

                 int i = sendEmail(SubjectForCase, emailBodys, emails, CC);

                return Json(requestApproval.RequestApprovalId);
            }
            

        }

        [HttpPost]
        public JsonResult ApproveDetailsJson(List<CuttingSheetDetail> Details)
        {
            string errors11 = "";
            int aaa = 0;
            int test = 0;
            int eid = 0;
            CuttingSheetDetail EP = new CuttingSheetDetail();


            foreach (var d in Details)
            {
                CuttingSheetDetail csd = db.CuttingSheetDetails.Find(d.CuttingSheetDetailId);
                csd.qtyApproved = d.qtyApproved;
                csd.Remark = d.Remark;

                csd.Approval = d.Approval;
                db.SaveChanges();
                

            }
            return Json("Cutting Sheet Saved Successfully");


        }



        // GET: RequestApprovals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RequestApproval requestApproval = db.RequestApprovals.Find(id);

            if (requestApproval == null)
            {
                return HttpNotFound();
            }
            if (!(requestApproval.CoordinatorFull || requestApproval.CoordinatorPartial))
            {
                return View(requestApproval);
            }
            else
            {
                string message = "Sorry ,  You do not have permission to Delete . <br/> " +
                                 "it is already approved. <br/> " +
                                 "Contact management ";
                return ErrorPermission(message);
            }
        }

        public ActionResult ErrorPermission(string message)
        {
            ViewBag.Message = message;
            return View("ErrorPermission");
        }
        // POST: RequestApprovals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestApproval requestApproval = db.RequestApprovals.Find(id);
            db.RequestApprovals.Remove(requestApproval);
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
