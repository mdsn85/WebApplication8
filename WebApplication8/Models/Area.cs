using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        [Display(Name = "Area")]
        [StringLength(50)]
        public String Name { get; set; }


        public virtual ICollection<Project> Projects { get; set; }

        public Area()
        {
            Projects = new Collection<Project>();
        }
    }
}