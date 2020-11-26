using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class CreditTerm
    {
        public int CreditTermId { get; set; }

        [Display(Name = "Credit Term")]
        public string Name { get; set; }
    }
}