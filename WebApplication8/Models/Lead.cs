using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Lead
    {
        public enum Months { JAN , FEB , MAR , APR, MAY, JUN, JUL ,AUG ,SEP, OCT, NOV, DEC}

        public int LeadId { get; set; }
        public DateTime DateRecieve { get; set; }
       
        [MaxLength(100)]
        [Display(Name= " Company Name / Client Name")]
        public string Name { get; set; }

        [MaxLength(100)]
        [Display(Name= "Contact Person")]
        public string ContactPerson { get; set; }

        [MaxLength(20)]
        [Display(Name = "Telephone")]
        public string Landline { get; set; }

        [MaxLength(20)]
        [Display(Name = "Mobile")]
        public string Mobile  { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }

        [StringLength(300)]
        [Display(Name = "Description")]
        public string Address { get; set; }

        [Display(Name = "Client / Company - Industry")]
        public int? NatureOfBusinessId { get; set; }
        public virtual NatureOfBusiness NatureOfBusiness { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string description { get; set; }



        [StringLength(500)]
        public string Remarks { get; set; }

        [Display(Name = "Sales Person Name")]
        public int? SalesManId { get; set; }
        public virtual SalesMan SalesMan { get; set; }

        [Display(Name = "Last Meeting Date")]
        public DateTime? LastMeetingDate { get; set; }

        [Display(Name = "Closuer Month")]
        public Months? ClosuerMonth { get; set; }

        [Display(Name = "Remarks Salesman")]
        public string RemarksSalesman { get; set; }
        [Display(Name = "Created by")]
        public string WhoCreated { get; set; }
        [Display(Name = "Created in")]
        public  DateTime DateOfCreate { get; set; }
        [Display(Name = "last update by")]
        public string WhoLastUpdate { get; set; }
        [Display(Name = "Last Update in")]
        public  DateTime? DateOfUpdate { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        [Display(Name = "Lead Status")]
        public int StatusOFLeadId { get; set; }
        public virtual StatusOFLead StatusOFLead { get; set; }

        [Display(Name = "Mode Of Lead")]
        public int ModeOfLeadId { get; set; }
        public virtual ModeOfLead ModeOfLead { get; set; }

           
        public virtual ICollection<LeadFile> LeadFiles { get; set; }


        public Lead()
        {
            LeadFiles = new Collection<LeadFile>();
        }


        [Display(Name = "Stamp Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? StampDate { get; set; }

        
        [Display(Name = "Total Quotation Value(Inc VAT)")]
        [Column(TypeName = "money")]
        public decimal? TotalQuotationValueIncVAT { get; set; }


    }



}