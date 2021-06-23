namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalQoutationValueLead : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leads", "TotalQuotationValueIncVAT", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leads", "TotalQuotationValueIncVAT");
        }
    }
}
