using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class CuttingSheetFile
    {
        public int CuttingSheetFileId { get; set; }
        public String Name { get; set; }
        public String Path { get; set; }


        public DateTime? ValidUntil { get; set; }

        public virtual CuttingSheet CuttingSheet { get; set; }
        public int? CuttingSheetId { get; set; }
    }
}