using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    //Material Category	Material Type	Material Description

    public class Material
    {
        public int MaterialId { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Material Description")]
        [StringLength(250)]
        public string Name { get; set; }


        

        [Display(Name = "Min ReOrder")]
        public int? MinReOrder { get; set; }

        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public float? qty { get; set; }

        [Display(Name = "Quantity")]
        public float OpeningQty { get; set; }



        [Display(Name = "AvalableQty")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float AvalableQty
        {
            get { return qty - MinReOrder - Resevedqty ?? 0; }
            private set { /* needed for EF */ }
        }

        [Display(Name = "Reseved Quantity")]
        public float? Resevedqty { get; set; }

        [Display(Name = "Avrage Price")]
        public float? AvgPrice { get; set; }

        [Display(Name = "latest Price")]
        public float? latestPrice { get; set; }

        public string barcode { get; set; }

        public string code { get; set; }


        public string Dimension { get; set; }

        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        [Display(Name = "Unit2")]
        public int? UnitId2 { get; set; }
       // public virtual Unit Unit2 { get; set; }

        public float? convert { get; set; }


        [Display(Name = "WareHouse")]
        public int WareHouseId { get; set; }
        public virtual Warehouse WareHouse { get; set; }


        public virtual ICollection<QuotaionProduct> QuotaionProducts { get; set; }


        public virtual ICollection<MaterialUnit> MaterialUnits { get; set; }

        public int? MaterialCategoryId { get; set; }
        public virtual MaterialCategory MaterialCategory { get; set; }

        public int? MaterialTypeId { get; set; }
        public virtual MaterialType MaterialType { get; set; }


    }
}