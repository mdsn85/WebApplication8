using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class FolderPath
    {
        public const String FolderPathCustomerDoc = "~/Uploads/CustomerDoc/";
        public const String FolderPathEmailDoc = "~/Uploads/emails/";
        public const String SignaturePath = "~/Uploads/CustomerDoc/";

        public const int NearExpiryDays = 30;
        public const int OutGoingContract = 6;

        public const String RemarkSecurityDeposit = "*Security Deposit amount is refundable at the time of Contract Termination /Contract Cancellation / Project Completion, once the customer’s account is clear.";

        public const String RemarkDMDisposal = "***The tipping fees will be applicable subject to the implementation of executive council resolution no: 58 from Dubai Municipality.";
        public const String Compacter = "";
        public static readonly String[] allowedExtensions = {".png", ".jpg", ".jpeg",".pdf",".doc",".docx",".xls",".xlsx",".doc",".docs" };
    }
}