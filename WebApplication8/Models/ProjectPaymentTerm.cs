using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class ProjectPaymentTerm

    {

        public int ProjectPaymentTermId { get; set; }

        [Display(Name = "Payment Term ")]
        public string Name { get; set; }
    }
}