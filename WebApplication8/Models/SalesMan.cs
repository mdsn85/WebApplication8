using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class SalesMan
    {
        public int SalesManId { get; set; }

        [Display(Name = "Sales Person")]
        public string Name { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }



        public int? EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }

        //team he is belong to 
        [Display(Name = "Sales Team")]
        public int? SalesTeamId { get; set; }
        public virtual SalesTeam SalesTeams { get; set; }

        [Display(Name = "Is Team Leader")]
        public bool isTeamleader { get; set; }
    }
}