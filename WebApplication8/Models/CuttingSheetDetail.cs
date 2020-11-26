using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{

    public enum statusList { InStock, Purchase, UnderView };
    public class CuttingSheetDetail
    {

        public int CuttingSheetDetailId { get; set; }


        public int MaterialId { get; set; }
        public virtual Material material { get; set; }

        public float qty { get; set; }


        public float qtyApproved { get; set; }

        public string Remark { get; set; }

        public bool Approval { get; set; }

        public double? IssueQty { get; set; }
        //public float Price { get; set; }

        public statusList status { get; set; }



        public int CuttingSheetId { get; set; }
        public virtual CuttingSheet CuttingSheet { get; set; }

    }
}