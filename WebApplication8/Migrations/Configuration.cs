namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication8.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication8.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.SalesTypes.AddOrUpdate(x => x.SalesTypeId,
                new SalesType() { SalesTypeId = 1, Name = "TRADE" },
                new SalesType() { SalesTypeId = 2, Name = "CUSTOMIZED" },
                new SalesType() { SalesTypeId = 3, Name = "TRADE+CUST" });




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
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = RoleNames.ROLE_LPOPrint }
                );






            context.ApprovalTypes.AddOrUpdate(x => x.ApprovalTypeId,
                 new ApprovalType() { ApprovalTypeId = 1, Name = "Extra Issue Material" }

            );


            context.EmailServers.AddOrUpdate(x => x.EmailServerId,
                new EmailServer() { EmailServerId = 1, Name = "Main Server", EnableSsl = true , Host= "smtp.office365.com", Port = 587 }
             );

        }
    }
}
