using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class CuttingSheet
    {
        public int CuttingSheetId { get; set; }

        [Display(Name = "Cutting Sheet Code")]
        public string code { get; set; }


        [Display(Name = "Project Code")]
        public int ProjectId { get; set; }
        public virtual Project project { get; set; }

        public int? Project2Id { get; set; }
        public virtual Project2 project2 { get; set; }

        [Display(Name = "Who Created")]
        public String UserCreate { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }


        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }

        public virtual ICollection<CuttingSheetDetail> CuttingSheetDetails { get; set; }
        public virtual ICollection<StockIssue> StockIssues { get; set; }

    }
}