using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }

        [Display(Name = "Ware House")]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}