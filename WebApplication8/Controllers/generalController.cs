using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Http;
using WebApplication8.Controllers.Resources;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class generalController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();




        [HttpGet]
        public IEnumerable<CuttingSheetDropDown> GetCuttingSheetForDropDown()
        {
            string[] copletedProject = { "COMPLETE", "DELIVERED", "CANCEL" }; 
            var cuttingSheet = db.CuttingSheets.Where(c=> !copletedProject.Contains( c.project.ProjectStatus.Name) )
                .Select(c => new CuttingSheetDropDown
                                { code = c.code,
                                    CuttingSheetId = c.CuttingSheetId,
                                    projectName = c.project.Name,
                                    QutationCode = c.project.QuotationRef??"" });

            return cuttingSheet.ToList();
        }
    }
}
