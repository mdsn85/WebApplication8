using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Quotation
    {
        public int QuotationId { get; set; }

        public string Code { get; set; }

        public virtual Customer Customer { get; set; }
        public int? CustomerId { get; set; }

        public int SalesManId { get; set; }
        public virtual SalesMan SalesMan { get; set; }
    }
}