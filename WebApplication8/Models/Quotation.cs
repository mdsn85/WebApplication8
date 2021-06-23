using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{

    public class Quotation
    {
        public int QuotationId { get; set; }

        public string Code { get; set; }


        public decimal Total { get; set; }

        public decimal Vat { get; set; }

        public decimal FinalPrice { get; set; }

        public virtual Lead Lead { get; set; }
        public int? LeadId { get; set; }

        public virtual Customer Customer { get; set; }
        public int? CustomerId { get; set; }

        public int SalesManId { get; set; }
        public virtual SalesMan SalesMan { get; set; }

        public DateTime Date { get; set; }
        public string QuotationRef { get; set; }

        public int DesignerId { get; set; }
        public  Designer Designer { get; set; }

        public int BankAccountDetailId { get; set; }
        public  BankAccountDetail BankAccountDetail { get; set; }

        public int DeliveryAreaId { get; set; }
        public  Area DeliveryArea { get; set; }

        [Display(Name = "Payment Term ")]
        public int ProjectPaymentTermId { get; set; }
        public ProjectPaymentTerm ProjectPaymentTerm { get; set; }

        [Display(Name = "Mode Of Payment")]
        public ModOfPaymentList ModOfPayment { get; set; }



        [Display(Name = "Create Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }


        [StringLength(20)]
        [Display(Name = "Who Created")]
        public String UserCreate { get; set; }


        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }


        [Display(Name = "Last Update Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:dd}", ApplyFormatInEditMode = true)]



        public DateTime? LastUpdateDate { get; set; }


        //public ICollection<Product> Products { get; set; }
        // public List<QuotaionProduct> QuotaionProducts { get; set; }

        public ICollection<QuotationTerms> QuotationTermss { get; set; }

        public ICollection<QuotaionProduct> QuotaionProducts { get; set; }




        public Quotation()
        {
            QuotationTermss =new  Collection<QuotationTerms>();
            QuotaionProducts = new List<QuotaionProduct>();
        }


    }
}