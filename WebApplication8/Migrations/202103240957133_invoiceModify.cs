namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceModify : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceSequenses",
                c => new
                    {
                        InvoiceSequenseId = c.Int(nullable: false, identity: true),
                        Sequense = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceSequenseId);
            
            AddColumn("dbo.Invoices", "UserCreate", c => c.String(maxLength: 20));
            AddColumn("dbo.Invoices", "StampDate", c => c.DateTime());
            AddColumn("dbo.Invoices", "Sequense", c => c.Int());
            AlterColumn("dbo.Invoices", "Code", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Invoices", "DeliverNote", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Invoices", "DespatchedThrough", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "DespatchedThrough", c => c.String(maxLength: 40));
            AlterColumn("dbo.Invoices", "DeliverNote", c => c.String(maxLength: 40));
            AlterColumn("dbo.Invoices", "Code", c => c.String(maxLength: 20));
            DropColumn("dbo.Invoices", "Sequense");
            DropColumn("dbo.Invoices", "StampDate");
            DropColumn("dbo.Invoices", "UserCreate");
            DropTable("dbo.InvoiceSequenses");
        }
    }
}
