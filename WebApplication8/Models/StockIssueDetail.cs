using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class StockIssueDetail
    {
        public int StockIssueDetailId { get; set; }

        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        public float InQty { get; set; }
        public float OutQty { get; set; }

       // public float? BalanceQty { get; set; }

        public float Price { get; set; }

        public int StockIssueId { get; set; }
        public virtual StockIssue StockIssue { get; set; }
    }
}