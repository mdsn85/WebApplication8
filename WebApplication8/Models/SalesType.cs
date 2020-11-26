using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class SalesType
    {

        public int SalesTypeId { get; set; }
        [Display(Name = "Sales Type")]
        public string Name { get; set; }
    }
}