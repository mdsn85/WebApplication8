using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Controllers.Resources
{
    public class MaterialFull
    {
        public int MaterialId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Name { get; set; }

        public int? MinReOrder { get; set; }

        public string Description { get; set; }

        public float? qty { get; set; }

        public float? OpeningQty { get; set; }

        public float? AvalableQty { get; set; }

        public float? Resevedqty { get; set; }

        public float? AvgPrice { get; set; }

        public float? latestPrice { get; set; }

        public string barcode { get; set; }

        public string code { get; set; }

        public string Dimension { get; set; }

        public int? UnitId2 { get; set; }
        // public virtual Unit Unit2 { get; set; }

        public float? convert { get; set; }

        public KeyValuePair Unit { get; set; }

        public KeyValuePair WareHouse { get; set; }

        public KeyValuePair MaterialCategory { get; set; }

        public KeyValuePair  MaterialType { get; set; }
    }
}