using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class NatureOfBusiness
    {
        public int NatureOfBusinessId { get; set; }
        [Display(Name = "Nature Of Business")]
        public String Name { get; set; }
    }
}