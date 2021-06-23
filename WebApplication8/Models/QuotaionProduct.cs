using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class QuotaionProduct
    {
        [MaxLength(10)]
        public string Dimensions { get; set; }
        [MaxLength(10)]
        public string Thickness { get; set; }
        [MaxLength(300)]
        public string Details { get; set; }

        public int Qty { get; set; }

        //Convert.ToDecimal(number).ToString("#,##0.00");
        public decimal Price { get; set; }


        public int MaterialId { get; set; }
        public Material Material { get; set; }

        [Key, Column(Order = 0)]
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public virtual ICollection<QuotaionProductFile> QuotaionProductFile { get; set; }



    }
}