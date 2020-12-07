using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class MaterialType
    {
        public int MaterialTypeId { get; set; }
        [Display(Name = "Type")]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public MaterialType()
        {
            Materials = new Collection<Material>();
        }
    }
}