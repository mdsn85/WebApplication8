namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoiceproductjointable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceProducts",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Single(nullable: false),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.InvoiceId, t.ProductId })
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InvoiceProducts", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.InvoiceProducts", new[] { "ProductId" });
            DropIndex("dbo.InvoiceProducts", new[] { "InvoiceId" });
            DropTable("dbo.InvoiceProducts");
        }
    }
}
