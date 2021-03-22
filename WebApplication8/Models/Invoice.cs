using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Display(Name = "Invoice No.")]
        [StringLength(20)]
        public string Code { get; set; }

        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate{ get; set; }


        [Display(Name = "Deliver Note")]
        [StringLength(40)]
        public string DeliverNote { get; set; }

        [Display(Name = "Despatched Through")]
        [StringLength(40)]
        public string DespatchedThrough { get; set; }


        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Display(Name = "Customer Reference")]
        [StringLength(60)]
        public string CustomerReference { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Total { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Discount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Vat { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal TotalNet { get; set; }


        public string TermsOfDelivery { get; set; }
        public string Remarks { get; set; }
        public string BankDetails { get; set; }
        public string Declaration { get; set; }

        public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; }

    }
}