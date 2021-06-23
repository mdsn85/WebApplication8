namespace WebApplication8.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;
    using log4net;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication8.Models.ApplicationDbContext>
    {
        ILog log = LogManager.GetLogger("application-log");

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication8.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            List<string> countries = GenerateCountryName();
            log.Info("vcountries:" + countries.Count());

            if (countries.Count()> context.Countries.Count())
            {
                int i = 1;
                foreach(string country in countries)
                {
                    context.Countries.AddOrUpdate(x => x.CountryId,
                    new Country() { CountryId = i, Name = country });
                    i++;
                }
            }

            context.SalesTypes.AddOrUpdate(x => x.SalesTypeId,
                new SalesType() { SalesTypeId = 1, Name = "TRADE" },
                new SalesType() { SalesTypeId = 2, Name = "CUSTOMIZED" },
                new SalesType() { SalesTypeId = 3, Name = "TRADE+CUST" });



           context.ModeOfLeads.AddOrUpdate(x => x.ModeOfLeadId,
                new ModeOfLead() { ModeOfLeadId = 1, Name = "Email" },
                new ModeOfLead() { ModeOfLeadId = 2, Name = "Website Chat" },
                new ModeOfLead() { ModeOfLeadId = 3, Name = "Whats App" },
                new ModeOfLead() { ModeOfLeadId = 4, Name = "Landline Al Quoz" },
                new ModeOfLead() { ModeOfLeadId = 5, Name = "Landline Oud Metha" },
                new ModeOfLead() { ModeOfLeadId = 6, Name = "Mobile Phone" },
                new ModeOfLead() { ModeOfLeadId = 7, Name = "Self Hunt" },
                new ModeOfLead() { ModeOfLeadId = 8, Name = "Client Referrals" },
                new ModeOfLead() { ModeOfLeadId = 9, Name = "Walk ins - Oud Metha" },
                new ModeOfLead() { ModeOfLeadId = 10, Name = "Walk ins - Al Quoz" },
                new ModeOfLead() { ModeOfLeadId = 11, Name = "Existing Client" },
                new ModeOfLead() { ModeOfLeadId = 12, Name = "Others - Please Specify in Remarks" },


                new ModeOfLead() { ModeOfLeadId = 13, Name = "Social Media Lead" },
                new ModeOfLead() { ModeOfLeadId = 14, Name = "EWebsite Backend Quotation Page" },
                new ModeOfLead() { ModeOfLeadId = 15, Name = "Website Backend Enquiry Page" }
                );

            context.Leads.AddOrUpdate(x => x.LeadId,
                new Lead() { LeadId = 1, Address = "test Address", Name = "Test Leads" , ClosuerMonth  = Lead.Months.APR,
                StatusOFLeadId=1,ModeOfLeadId=1,DateOfCreate=DateTime.Now,DateRecieve=DateTime.Now});
                

            context.StatusOFLeads.AddOrUpdate(x => x.StatusOFLeadId,
                new StatusOFLead() { StatusOFLeadId = 1, Name = "Targeted" },
                new StatusOFLead() { StatusOFLeadId = 2, Name = "Active" },
                new StatusOFLead() { StatusOFLeadId = 3, Name = "Hot" }, 
                new StatusOFLead() { StatusOFLeadId = 4, Name = "Closed" },
                new StatusOFLead() { StatusOFLeadId = 5, Name = "Lost" },
                new StatusOFLead() { StatusOFLeadId = 6, Name = "Future Lead" },
                new StatusOFLead() { StatusOFLeadId = 7, Name = "Lead Not Useful" }
            );



            context.LpoNatures.AddOrUpdate(x => x.LpoNatureId,
                new LpoNature() { LpoNatureId = 1, Name = "factory / warehouse" },
                new LpoNature() { LpoNatureId = 2, Name = "showroom O/M" },
                new LpoNature() { LpoNatureId = 3, Name = "showroom AQ" },
                new LpoNature() { LpoNatureId = 4, Name = "projects" });


            context.MaterialCategorys.AddOrUpdate(x => x.MaterialCategoryId,
                new MaterialCategory() { MaterialCategoryId = 1, Name = "Fast Moving" },
                new MaterialCategory() { MaterialCategoryId = 2, Name = "Raw Material" },
                new MaterialCategory() { MaterialCategoryId = 3, Name = "Safety" },
                new MaterialCategory() { MaterialCategoryId = 4, Name = "Tools" },
                new MaterialCategory() { MaterialCategoryId = 5, Name = "Trading" });


            context.MaterialTypes.AddOrUpdate(x => x.MaterialTypeId,
                new MaterialType() { MaterialTypeId = 1, Name = "Accessories" },
                new MaterialType() { MaterialTypeId = 2, Name = "Packing" },
                new MaterialType() { MaterialTypeId = 3, Name = "Consumables" },
                new MaterialType() { MaterialTypeId = 4, Name = "Acrylic" },
                new MaterialType() { MaterialTypeId = 5, Name = "Boards" },
                new MaterialType() { MaterialTypeId = 6, Name = "Edge Band" },
                new MaterialType() { MaterialTypeId = 7, Name = "Fabrication" },
                new MaterialType() { MaterialTypeId = 8, Name = "PPE" },
                new MaterialType() { MaterialTypeId = 9, Name = "Hand Tools" },
                new MaterialType() { MaterialTypeId = 10, Name = "Power Tools" },
                new MaterialType() { MaterialTypeId = 11, Name = "Spare Parts" },
                new MaterialType() { MaterialTypeId = 12, Name = "Carpets" },
                new MaterialType() { MaterialTypeId = 13, Name = "Finished Goods" },
                new MaterialType() { MaterialTypeId = 14, Name = "Flooring" },
                new MaterialType() { MaterialTypeId = 15, Name = "Glass" }
                );


            context.NatureOfBusinesses.AddOrUpdate(x => x.NatureOfBusinessId,
                new NatureOfBusiness() { NatureOfBusinessId = 1, Name = "Manufacturing" },
                new NatureOfBusiness() { NatureOfBusinessId = 2, Name = "Banking" },
                new NatureOfBusiness() { NatureOfBusinessId = 3, Name = "FM / Real Estate" },
                new NatureOfBusiness() { NatureOfBusinessId = 4, Name = "Construction" },
                new NatureOfBusiness() { NatureOfBusinessId = 5, Name = "Interiors" },
                new NatureOfBusiness() { NatureOfBusinessId = 6, Name = "Super Market" },
                new NatureOfBusiness() { NatureOfBusinessId = 7, Name = "Mall" },
                new NatureOfBusiness() { NatureOfBusinessId = 8, Name = "Restaurant" },
                new NatureOfBusiness() { NatureOfBusinessId = 9, Name = "Garage / Workshops" },
                new NatureOfBusiness() { NatureOfBusinessId = 10, Name = "Logistics" },
                new NatureOfBusiness() { NatureOfBusinessId = 11, Name = "Transport" },
                new NatureOfBusiness() { NatureOfBusinessId = 12, Name = "Contracting " },
                new NatureOfBusiness() { NatureOfBusinessId = 13, Name = "General Trading" },
                new NatureOfBusiness() { NatureOfBusinessId = 14, Name = "Trading" },
                new NatureOfBusiness() { NatureOfBusinessId = 15, Name = "Hotel" },



                new NatureOfBusiness() { NatureOfBusinessId = 16, Name = "Hotel Apartment " },
                new NatureOfBusiness() { NatureOfBusinessId = 17, Name = "Clinic" },
                new NatureOfBusiness() { NatureOfBusinessId = 18, Name = "Hospital" },
                new NatureOfBusiness() { NatureOfBusinessId = 19, Name = "Event Management" },
                new NatureOfBusiness() { NatureOfBusinessId = 20, Name = "School" },
                new NatureOfBusiness() { NatureOfBusinessId = 21, Name = "University" },
                new NatureOfBusiness() { NatureOfBusinessId = 22, Name = "Catering" },

                new NatureOfBusiness() { NatureOfBusinessId = 23, Name = "Printing" },
                new NatureOfBusiness() { NatureOfBusinessId = 24, Name = "Fabrication" },
                new NatureOfBusiness() { NatureOfBusinessId = 25, Name = "Storage Facility" },
                new NatureOfBusiness() { NatureOfBusinessId = 26, Name = "Others" }
                );


            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_MRFView},
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_MRFCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_MRFEdit },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_MRFPrint }
                );

            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectDownloadExcel },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectEdit },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectView },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectViewAttachment },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectViewCustomize },

                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectAddPayment },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProjectViewPayment },

                 new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_Account });

            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_CustomerCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_CustomerEdit },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_CustomerView }
                );

            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_LPOCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_LPOEdit },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_LPOView },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_LPOPrint },

                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_LPORevise },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_LPODownloadExcel }
                
                );


            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_MaterialCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_MaterialEdit },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_MaterialView }
                );


            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_StockIssuesCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_StockIssuesEdit },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_StockIssuesView }
                );



            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_SupplierEdit },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_SupplierCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_SupplierView }
                );

            context.Roles.AddOrUpdate(x => x.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProdutCreate },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProdutView },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_ProdutEdit }
                );
            context.Roles.AddOrUpdate(x => x.Name,
                    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_SalesMudole }
            );


            context.Roles.AddOrUpdate(x => x.Name,
                    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_InvoiceCreate },
                    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_InvoiceView },
                    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_InvoicePrint },
                    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_InvoiceDownload }
                    );

            //context.NotificationCategorys.AddOrUpdate(x => x.Name,
            //    new NotificationCategory() {  Name = NotificationName.onAccountApproval },
            //    new NotificationCategory() {  Name = NotificationName.onCreateProject },
            //    new NotificationCategory() { Name = NotificationName.onCreateMRF },
            //    new NotificationCategory() {  Name = NotificationName.onCreateLpo },
            //    new NotificationCategory() { Name = NotificationName.onStockIn },
            //    new NotificationCategory() {  Name = NotificationName.onStockOut }

            //    );




            context.ApprovalTypes.AddOrUpdate(x => x.ApprovalTypeId,
                 new ApprovalType() { ApprovalTypeId = 1, Name = "Extra Issue Material" }

            );


        context.EmailServers.AddOrUpdate(x => x.EmailServerId,
                new EmailServer() { EmailServerId = 1, Name = "Main Server", EnableSsl = true , Host= "smtp.office365.com", Port = 587 }
             );




           

        }

        private List<string> GenerateCountryName()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }

            CountryList.Sort();

            return CountryList;
        }
    }
}
