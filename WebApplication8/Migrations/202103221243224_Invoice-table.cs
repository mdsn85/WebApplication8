namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoicetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        InvoiceDate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        DeliverNote = c.String(maxLength: 40),
                        DespatchedThrough = c.String(maxLength: 40),
                        ProjectId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CustomerReference = c.String(maxLength: 60),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalNet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TermsOfDelivery = c.String(),
                        Remarks = c.String(),
                        BankDetails = c.String(),
                        Declaration = c.String(),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                    })
                .PrimaryKey(t => t.ProductId);
            
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "Emirate", c => c.String());
            AddColumn("dbo.Customers", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.Invoices", new[] { "ProjectId" });
            DropColumn("dbo.Customers", "Country");
            DropColumn("dbo.Customers", "Emirate");
            DropColumn("dbo.Customers", "City");
            DropTable("dbo.Products");
            DropTable("dbo.Invoices");
        }
    }
}
