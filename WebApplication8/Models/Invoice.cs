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

        [Required]
        [Display(Name = "Invoice No.")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate{ get; set; }

        [Required]
        [Display(Name = "Deliver Note")]
        [StringLength(40)]
        public string DeliverNote { get; set; }

        [Required]
        [Display(Name = "Despatched Through")]
        [StringLength(40)]
        public string DespatchedThrough { get; set; }

        [Required]
        [Display(Name = "Project Code")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        [Display(Name = "Customer Details")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Display(Name = "Customer Reference")]
        [StringLength(60)]
        public string CustomerReference { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal SubTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Discount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Vat { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal GrandTotal { get; set; }

        [Display(Name = "Who Created")]
        [StringLength(20)]
        public String UserCreate { get; set; }

        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }


        public int? Sequense { get; set; }

        public string TermsOfDelivery { get; set; }
        public string Remarks { get; set; }
        public string BankDetails { get; set; }
        public string Declaration { get; set; }

        public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; }

    }
}