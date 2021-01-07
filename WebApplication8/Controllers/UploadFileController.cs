
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class UploadFileController : Controller
    {
        ILog log = LogManager.GetLogger("myapp.log");

        static string[] allowedExtensions = FolderPath.allowedExtensions;

        public ActionResult IndexGoLegalCase()
        {
            
            return View(allowedExtensions);
        }

        public string uploadFileSingle(HttpPostedFileBase file)
        {



            //upload file check extention


            //*****************
           
            String FileName = "";
            if (file != null)
            {
                //save file to server

                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                ext = ext.ToLower();
                string OrginalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                if (!allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    ViewBag.message = "Please choose only Image or PDF file or Ms Office files";

                    return "Please choose only Image or PDF file or Ms Office files";
                }

                string RandomFileName = Path.GetRandomFileName();
                RandomFileName = RandomFileName.Substring(0, RandomFileName.Length - 4);
                FileName = OrginalFileName + RandomFileName + ext;
                var path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), FileName);
                file.SaveAs(path);
                ;
            }

            //  Send "Success"
            //return "done";
            return "{\"msg\":\"" +  FileName+ "\"}";

        }

        

        // call UploadFiles
        public ActionResult uploadFileMulti(string[] listOfFiles = null)
        {
            if (listOfFiles == null)
            {
                return View(allowedExtensions);
            }
            ViewBag.listOfFile = listOfFiles;
            return View(); ;
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            log.Info(" Start Appload File");
            try
            {
                List<string> filesName = new List<string>();
                //string uname = Request["uploadername"];
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    string fname;
                    // Checking for Internet Explorer      
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }



                    var ext = Path.GetExtension(fname); //getting the extension(ex-.jpg)  
                    ext = ext.ToLower();
                    string OrginalFileName = Path.GetFileNameWithoutExtension(fname);
                    if (!allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        return new HttpStatusCodeResult(410, "Please choose only Image or PDF file or Ms Office files.");

                    }

                    string RandomFileName = Path.GetRandomFileName();
                    RandomFileName = RandomFileName.Substring(0, RandomFileName.Length - 4);
                    string FileName = OrginalFileName + RandomFileName + ext;
                    var path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), FileName);
                    file.SaveAs(path);
                    filesName.Add(FileName);
                    // Get the complete folder path and store the file inside it.      
                    //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                    //file.SaveAs(fname);
                }

                return Json(filesName, JsonRequestBehavior.AllowGet);
            }catch (Exception e)
            {
                
                log.Error(" ERROR mylog - Error while upload file to server:"+e.Message + " , stacktrace:"+ e.StackTrace);
                return new HttpStatusCodeResult(410, e.Message + " . Please reupload or contact IT");
            }
           
        }
    
    [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public string uploadFileMulti(HttpPostedFileBase data)
        {



            //upload file check extention


            //*****************

            String FileName = "";
            if (data != null)
            {
                //save file to server
                var file = data;
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                ext = ext.ToLower();
                string OrginalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                if (!allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    ViewBag.message = "Please choose only Image or PDF file or Ms Office files";

                    return "Please choose only Image or PDF file or Ms Office files";
                }

                string RandomFileName = Path.GetRandomFileName();
                RandomFileName = RandomFileName.Substring(0, RandomFileName.Length - 4);
                FileName = OrginalFileName + RandomFileName + ext;
                var path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), FileName);
                file.SaveAs(path);

            }

            //  Send "Success"
            //return "done";
            return "{\"msg\":\"" + FileName + "\"}";

        }








        [HttpPost]
        public ActionResult UploadDoNum(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);


                //OleDbConnection conn = new OleDbConnection();
                //conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"Read data from Excel.xlsx" + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                //using (OleDbCommand comm = new OleDbCommand())
                //{
                //    comm.CommandText = "Select * from [" + "Do sheet" + "$A3:B6]";
                //    comm.Connection = conn;
                //    using (OleDbDataAdapter da = new OleDbDataAdapter())
                //    {
                //        da.SelectCommand = comm;
                //        da.Fill(dt);
                //    }
                //}
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "Select * from [" + "DoSheets$" + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.DoSheets";

                        //[OPTIONAL]: Map the Excel columns with that of the database table[DoNum]
                        sqlBulkCopy.ColumnMappings.Add("DoNum", "DoNum");
                        sqlBulkCopy.ColumnMappings.Add("StartDate", "StartDate");
                        sqlBulkCopy.ColumnMappings.Add("EndDate", "EndDate");
                        sqlBulkCopy.ColumnMappings.Add("ContractId", "ContractId");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {

            this.Response.AddHeader("Content-Disposition", "Grid.xlsx");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(GridHtml);



            return File(buffer, "application/vnd.ms-excel", "Grid.xlsx");




            // FileStreamResult a = File(Encoding.UTF8.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
            //return File(Encoding.UTF8.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
        }
        public ActionResult IndexGoogle()
        {
            return View();
        }

        public ActionResult IndexGo()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public void UploadFileForEdit(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // TODO: need to define destination
                        var path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), fileName);
                        file.SaveAs(path);
                    }
                }
            }
        }


        [HttpPost]
        public ActionResult UploadFile()
        {
            try
            {
                HttpPostedFileBase hpf = HttpContext.Request.Files["file"] as HttpPostedFileBase;
                string tag = HttpContext.Request.Params["tags"];// The same param name that you put in extrahtml if you have some.
                string category = HttpContext.Request.Params["category"];

                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath(FolderPath.FolderPathCustomerDoc));// If you don't have the folder yet, you need to create.
                string sentFileName = Path.GetFileName(hpf.FileName); //it can be just a file name or a user local path! it depends on the used browser. So we need to ensure that this var will contain just the file name.
                string savedFileName = Path.Combine(di.FullName, sentFileName);
                hpf.SaveAs(savedFileName);

                var msg = new { msg = "File Uploaded", filename = hpf.FileName, url = savedFileName };
                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }
        [Route("{url}")]
        public ActionResult DownloadFile(string url)
        {
            return File(url, "application/pdf");
        }

        [HttpPost]
        public ActionResult DeleteFile(string url)
        {
            try
            {
                System.IO.File.Delete(url);
                var msg = new { msg = "File Deleted!" };
                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }

        [HttpPost]
        public ActionResult UploadFileGoogle()
        {
            try
            {
                HttpPostedFileBase hpf = HttpContext.Request.Files["file"] as HttpPostedFileBase;
                string tag = HttpContext.Request.Params["tags"];// The same param name that you put in extrahtml if you have some.
                string category = HttpContext.Request.Params["category"];

                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Uploads/Decision"));// If you don't have the folder yet, you need to create.
                string sentFileName = Path.GetFileName(hpf.FileName); //it can be just a file name or a user local path! it depends on the used browser. So we need to ensure that this var will contain just the file name.
                string savedFileName = Path.Combine(di.FullName, sentFileName);
                hpf.SaveAs(savedFileName);

                var msg = new { msg = "File Uploaded", filename = hpf.FileName, url = savedFileName };
                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }

       

        public string uploadFileMultiWithType(HttpPostedFileBase file)
        {

            // OpinionFiles OpinionFile = new OpinionFiles();

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

                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                ext = ext.ToLower();
                string OrginalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                if (!allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    ViewBag.message = "Please choose only Image or PDF file";

                    return "Please choose only Image or PDF file";
                }

                string RandomFileName = Path.GetRandomFileName();
                RandomFileName = RandomFileName.Substring(0, RandomFileName.Length - 4);
                FileName = OrginalFileName + DateTime.Now.ToString("dd MMMM yyyy") + ext;
                var path = Path.Combine(Server.MapPath(FolderPath.FolderPathCustomerDoc), FileName);
                file.SaveAs(path);

            }

            //  Send "Success"
            //return "done";



            return "{\"msg\":\"" + FileName + "\"}";

        }
    }
}