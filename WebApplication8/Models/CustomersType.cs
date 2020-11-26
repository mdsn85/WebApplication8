using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class CustomersType
    {
        public int CustomersTypeId { get; set; }

        [Display(Name = "Customer Type")]
        public string Name { get; set; }

    }
}