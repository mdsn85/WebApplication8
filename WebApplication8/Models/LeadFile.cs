using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class LeadFile
    {


        public int LeadFileId { get; set; }
  

        public String Name { get; set; }
        public String Path { get; set; }


        public DateTime? ValidUntil { get; set; }

        public virtual Lead Lead { get; set; }
        public int? LeadId { get; set; }
    }
}