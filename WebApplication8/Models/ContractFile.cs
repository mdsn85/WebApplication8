using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class ContractFile
    {
        public int ContractFileId { get; set; }
        public String Name { get; set; }
        public String Path { get; set; }


        public DateTime ValidUntil { get; set; }

        public virtual Project Project { get; set; }
        public int? ProjectId { get; set; }
    }
}