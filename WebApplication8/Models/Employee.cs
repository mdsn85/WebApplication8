using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }
        public string Code { get; set; }

        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Display(Name = "Designation")]
        public string designation { get; set; }

        [Display(Name = "Email Signature")]
        public string EmailSignature { get; set; }






    }
}