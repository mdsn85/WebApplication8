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
        [StringLength(30)]
        public string code { get; set; }


        [Display(Name = "Status")]
        public int? LpoStatusId { get; set; }
        public virtual LpoStatus LpoStatus { get; set; }

        [Display(Name = "Who Created")]
        [StringLength(20)]
        public String UserCreate { get; set; }

        [Display(Name = "Quotation Ref#")]
        [StringLength(20)]
        public string QuotationRef { get; set; }

        [Display(Name = "Supplier Ref#")]
        [StringLength(30)]
        public string SupplierRef { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(200)]
        public string Remarks { get; set; }

        [Display(Name = "LPO Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LpoDate { get; set; }


        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        public virtual supplier Supplier { get; set; }

        [Display(Name = "Location")]
        public int? LpoLocationId { get; set; }
        public virtual LpoLocation LpoLocation { get; set; }

        [Display(Name = "Nature")]
        public int? LpoNatureId { get; set; }
        [Display(Name = "Nature")]
        public virtual LpoNature LpoNature { get; set; }

        [Display(Name = "Credit Term")]
        public int? CreditTermSupplierId { get; set; }
        public virtual CreditTermSupplier CreditTermSupplier { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float SubTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Discount { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Vat { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float GrandTotal { get; set; }

        public int? Sequense { get; set; }

        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }

        public virtual ICollection<LpoDetail> LpoDetails { get; set; }

        public virtual ICollection<StockIssue> StockIssues { get; set; }

    }
}