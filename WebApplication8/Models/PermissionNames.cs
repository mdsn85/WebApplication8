using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class PermissionNames
    {
        [Display(Name = "Production Manager Approval")]
        public const string Permission_ProductionManager = "Production Manager";

        [Display(Name = "Sales Manager Approval")]
        public const string Permission_SalesManager = "SalesManager";

        [Display(Name = "Sales Manager Approval")]
        public const string Permission_BDM = "BdmManager";

        [Display(Name = "Sr.C.M Approval")]
        public const string Permission_CoordinatorFull = "CoordinatorFull";

        [Display(Name = "Mngt. Approval")]
        public const string Permission_CoordinatorPartial = "CoordinatorPartial";

        [Display(Name = "CEO Approval")]
        public const string Permission_CEO = "CEO";

        [Display(Name = "Trips Approval")]
        public const string Permission_Trips = "Operation Supervisor";

        [Display(Name = "Pullout Approval")]
        public const string Permission_Pullout = "Account Supervisor";
    }
}