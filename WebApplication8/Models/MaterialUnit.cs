using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class MaterialUnit
    {
        public int  MaterialUnitId{get;set;}

        [Display(Name = "Material")]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }


        public float convert { get; set; }
    }
}