using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Project2
    {

        public int Project2Id { get; set; }

        [Display(Name = "Project Name")]
        public string Name { get; set; }


        [Display(Name = "Project Code")]
        public string Code { get; set; }

        [Display(Name = "Project Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SalesDate { get; set; }


        [Display(Name = "Sales Man")]
        public int SalesManId { get; set; }
        public SalesMan SalesMan { get; set; }




        //public int SalesTypeId { get; set; }
       // public SalesType SalesType { get; set; }

        //public int CustomerTypeId { get; set; }
        //public CustomerType CustomerType { get; set; }



        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

       public int DesignerId { get; set; }
       public Designer Designer { get; set; }

        public float Value { get; set; }
        public float Discount { get; set; }
        public float Vat { get; set; }
        public float Total { get; set; }
        public float SalePrice { get; set; }

        public int ProjectPaymentTermId { get; set; }
        public ProjectPaymentTerm ProjectPaymentTerm { get; set; }

        public string Description { get; set; }


        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Actual Delivery Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ActualDeliveryDate { get; set; }


        public bool AccountApproval { get; set; }






        public virtual ICollection<CuttingSheet> CuttingSheets { get; set; }
    }
}