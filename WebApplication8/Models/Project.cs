using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Project
    {

        public int ProjectId { get; set; }

        [StringLength(150)]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "Project Code")]
        public string Code { get; set; }

        [Display(Name = "Project Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SalesDate { get; set; }


        [Display(Name = "Sales Man")]
        public int SalesManId { get; set; }
        public SalesMan SalesMan { get; set; }


        [Display(Name = "Sales Type")]
        public int SalesTypeId { get; set; }
        [Display(Name = "Sales Type")]
        public SalesType SalesType { get; set; }


         [Display(Name = "Customer Type")]
         public int CustomersTypeId { get; set; }
        [Display(Name = "Customer Type")]
        public CustomersType CustomersType { get; set; }


        [Display(Name = "Customer ")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        [Display(Name = "Designer ")]
        public int DesignerId { get; set; }
        public Designer Designer { get; set; }


        [Display(Name = "Project Status ")]
        public int ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        public float Value { get; set; }
        public float Discount { get; set; }
        public float Vat { get; set; }
        public float Total { get; set; }
        public float? SalePrice { get; set; }

        [Display(Name = "Deduction ")]
        public float? Deduction { get; set; }


        [StringLength(350)]
        [Display(Name = "Deduction Reason ")]
        public string DeductionReason { get; set; }



        [Display(Name = "Payment Term ")]
        public int ProjectPaymentTermId { get; set; }
        public ProjectPaymentTerm ProjectPaymentTerm { get; set; }


        [StringLength(350)]
        public string Description { get; set; }


        [Display(Name = "Expected Delivery Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Actual Delivery Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ActualDeliveryDate { get; set; }

        [Display(Name = "Account Approval")]
        public bool AccountApproval { get; set; }


        [Display(Name = "Create Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }


        [StringLength(20)]
        [Display(Name = "Who Created")]
        public String UserCreate { get; set; }


        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }


        [Display(Name = "Last Update Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastUpdateDate { get; set; }

        // 
        [StringLength(20)]
        [Display(Name = "Quotation Ref#")]
        public String QuotationRef { get; set; }
        //   QuotationAgreementApprovedby

        [StringLength(50)]
        [Display(Name = "Quo. Agreement Approved by")]
        public String QuotationAgreementApprovedby { get; set; }


        [StringLength(20)]
        [Display(Name = "Who Last Update")]
        public String UserLastUpdate { get; set; }

        public virtual ICollection<CuttingSheet> CuttingSheets { get; set; }

        public virtual ICollection<ProjectFile> ProjectFiles { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }


    }
}