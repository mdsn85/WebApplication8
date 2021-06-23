namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTotalInvoicedate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
