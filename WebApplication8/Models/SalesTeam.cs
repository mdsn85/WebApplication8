using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class SalesTeam
    {
        public int SalesTeamId { get; set; }
        public String Name { get; set; }

        //reporting to 
        [Display(Name = "Reporting To")]
        public int? EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }

        //team memebers
        public virtual ICollection<SalesMan> SalesMans { get; set; }
    }
}