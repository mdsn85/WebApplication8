using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Designer
    {
        public int DesignerId { get; set; }
        [Display(Name = "Designer ")]
        public string Name { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }



        public int? EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }
    }
}