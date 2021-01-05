using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class LpoLocation
    {

        [Display(Name = "Location")]
        public int LpoLocationId { get; set; }
        [Display(Name = "Location")]
        [StringLength(450)]
        public string Name { get; set; }

        public virtual ICollection<Lpo> Lpos { get; set; }
    }
}