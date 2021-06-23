using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class QuotationTerms
    {
        public int QuotationTermsId { get; set; }
        public string text { get; set; }

        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

    }
}