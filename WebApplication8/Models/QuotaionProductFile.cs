using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class QuotaionProductFile
    {

        public int QuotaionProductFileId { get; set; }
        public String Name { get; set; }
        public String Path { get; set; }



        public virtual QuotaionProduct QuotaionProduct { get; set; }
        public int ProductId { get; set; }
        public int QuotationId { get; set; }


    }
}