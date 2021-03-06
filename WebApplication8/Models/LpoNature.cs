﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class LpoNature
    {
        [Display(Name = "Lpo Nature")]
        public int LpoNatureId { get; set; }
        [Display(Name = "Lpo Nature")]
        [StringLength(450)]
        public string Name { get; set; }

        public virtual ICollection<Lpo> Lpos { get; set; }
    }
}