using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class ProjectStatus
    {
        public int ProjectStatusId { get; set; }

        [Display(Name = "Project Status ")]
        public string Name { get; set; }
    }
}