using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{

    public enum ModOfPaymentList { Cash, Chq, BT, POS };

    public enum TypeOfPaymentList { ADVANCE, PART, FINAL };

    public class Payment
    {
        public int PaymentId { get; set; }
        public string Receipt { get; set; }

        [Display(Name = "Mode Of Payment")]
        public ModOfPaymentList ModOfPayment { get; set; }

        [Display(Name = "Type Of Payment")]
        public TypeOfPaymentList TypeOfPayment { get; set; }

        public float Amount { get; set; }

        [Display(Name = "Collected By")]
        public string CollectedBy { get; set; }

        [Display(Name = "Collection Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CollectionDate { get; set; }

        [Display(Name = "Chq No")]
        public string ChqNo { get; set; }

        [Display(Name = "Chq Date")]
        public DateTime? ChqDate { get; set; }



        [Display(Name = "Transferred By")]
        public string TransBy { get; set; }

        [Display(Name = "Date of Credit in Bank")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfCredit { get; set; }

        [Display(Name = "To Bank - HM")]
        public string ToBankHM { get; set; }

        [Display(Name = "A/C # -HM")]
        public string ACHM { get; set; }


        [Display(Name = "Who Created")]
        public String UserCreate { get; set; }

        [Display(Name = "POS REF#")]
        public String PosRef { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }


        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }



    }
}