using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class StockIssue
    {
        public int StockIssueId { get; set; }


        [Display(Name = "StockIssue Type")]
        public int StockIssueTypeId { get; set; }
        public virtual StockIssueType StockIssueType { get; set; }

        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }

        [Display(Name = "LPO")]
        public int? LpoId { get; set; }
        public virtual Lpo Lpo { get; set; }

        [Display(Name = "Cutting Sheet")]
        public int? CuttingSheetId { get; set; }
        public virtual CuttingSheet CuttingSheet { get; set; }

        public string Code { get; set; }

        public string Ref { get; set; }

        [Display(Name = "Who Created")]
        public String UserCreate { get; set; }

        [Display(Name = "Issue Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; }


        [Display(Name = "Notes")]
        public String Note { get; set; }

        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }

        public virtual ICollection<StockIssueDetail> StockIssueDetails { get; set; }

    }
}