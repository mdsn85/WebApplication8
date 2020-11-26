using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Unit
    {
        public int UnitId { get; set; }

        [Display(Name = "Unit")]
        public string Name { get; set; }

    }
}