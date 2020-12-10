using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class CreditTermSupplier
    {
        public int CreditTermSupplierId { get; set; }


        [StringLength(50)]
        [Display(Name = "Credit Term")]
        public string Name { get; set; }

        public virtual ICollection<supplier> Suppliers { get; set; }

        public virtual ICollection<Lpo> Lpos { get; set; }

    }
}