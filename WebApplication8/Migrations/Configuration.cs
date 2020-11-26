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

            context.ApprovalTypes.AddOrUpdate(x => x.ApprovalTypeId,
                 new ApprovalType() { ApprovalTypeId = 1, Name = "Extra Issue Material" }

            );


            context.EmailServers.AddOrUpdate(x => x.EmailServerId,
                new EmailServer() { EmailServerId = 1, Name = "Main Server", EnableSsl = true , Host= "smtp.office365.com", Port = 587 }
             );

        }
    }
}
