using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class InvoiceProduct
    {
        public int InvoiceId { get; set; }

        public int ProductId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Product Product { get; set; }


        public float Qty { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float Price { get; set; }

    }
}