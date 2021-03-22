using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Product
    {

        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [StringLength(80)]
        public String Name { get; set; }

        public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; }
    }
}