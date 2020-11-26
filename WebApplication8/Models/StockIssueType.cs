using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public enum MoveType { InStore , OutStore };

    public class StockIssueType
    {
        public int StockIssueTypeId { get; set; }

        [Display(Name = "Stock Issue Type")]
        public string Name { get; set; }


        [Display(Name = "Move Type")]
        public MoveType? Type { get; set; }

        public virtual ICollection<StockIssue> StockIssues { get; set; }
    }
}