using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models.Fillter
{
    public class InvoiceFillter
    {
        public string SearchCode { get; set; }
        public string CustomerId { get; set; }
        public string ProjectId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}