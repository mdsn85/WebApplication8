using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class BankAccountDetail
    {
        public int BankAccountDetailId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string IBAN { get; set; }
        public string AccountNo { get; set; }
        public string SwiftCode { get; set; }
        public string RoutingCode { get; set; }
        public string Branch { get; set; }
        public string TRN { get; set; }

    }
}