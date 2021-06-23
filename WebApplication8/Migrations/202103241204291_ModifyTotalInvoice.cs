namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTotalInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoices", "GrandTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Invoices", "Total");
            DropColumn("dbo.Invoices", "TotalNet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "TotalNet", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoices", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Invoices", "GrandTotal");
            DropColumn("dbo.Invoices", "SubTotal");
        }
    }
}
