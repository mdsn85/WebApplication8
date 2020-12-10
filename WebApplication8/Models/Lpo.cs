using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Lpo
    {
        public int LpoId { get; set; }

        [Display(Name = "LPO Code")]
        public string code { get; set; }


        [Display(Name = "Status")]
        public int? LpoStatusId { get; set; }
        public virtual LpoStatus LpoStatus { get; set; }

        [Display(Name = "Who Created")]
        public String UserCreate { get; set; }

        [Display(Name = "Quotation Ref#")]
        public string QuotationRef { get; set; }

        [Display(Name = "Supplier Ref#")]
        public string SupplierRef { get; set; }

        [Display(Name = "LPO Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LpoDate { get; set; }


        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public virtual supplier Supplier { get; set; }


        [Display(Name = "Credit Term")]
        public int CreditTermId { get; set; }
        public virtual CreditTermSupplier CreditTerm { get; set; }

        public float SubTotal { get; set; }

        public float Discount { get; set; }

        public float Vat { get; set; }

        public float GrandTotal { get; set; }

        public int? Sequense { get; set; }

        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }

        public virtual ICollection<LpoDetail> LpoDetails { get; set; }

        public virtual ICollection<StockIssue> StockIssues { get; set; }

    }
}