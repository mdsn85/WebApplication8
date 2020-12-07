using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class MaterialCategory
    {
        public int MaterialCategoryId { get; set; }
        [Display(Name = "Category")]
        [StringLength(50)]
        public string  Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public MaterialCategory()
        {
            Materials = new Collection<Material>();
        }
    }
}