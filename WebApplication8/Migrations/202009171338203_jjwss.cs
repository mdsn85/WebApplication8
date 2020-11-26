namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jjwss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project1",
                c => new
                    {
                        Project1Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        SalesDate = c.DateTime(nullable: false),
                        SalesManId = c.Int(nullable: false),
                        SalesTypeId = c.Int(nullable: false),
                        CustomerTypeId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DesignerId = c.Int(nullable: false),
                        Value = c.Single(nullable: false),
                        Discount = c.Single(nullable: false),
                        Vat = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                        SalePrice = c.Single(nullable: false),
                        ProjectPaymentTermId = c.Int(nullable: false),
                        Description = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        ActualDeliveryDate = c.DateTime(nullable: false),
                        AccountApproval = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Project1Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.CustomerTypes", t => t.CustomerTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Designers", t => t.DesignerId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectPaymentTerms", t => t.ProjectPaymentTermId, cascadeDelete: true)
                .ForeignKey("dbo.SalesMen", t => t.SalesManId, cascadeDelete: true)
                .ForeignKey("dbo.SalesTypes", t => t.SalesTypeId, cascadeDelete: true)
                .Index(t => t.SalesManId)
                .Index(t => t.SalesTypeId)
                .Index(t => t.CustomerTypeId)
                .Index(t => t.CustomerId)
                .Index(t => t.DesignerId)
                .Index(t => t.ProjectPaymentTermId);
            
            AddColumn("dbo.CuttingSheets", "Project1_Project1Id", c => c.Int());
            CreateIndex("dbo.CuttingSheets", "Project1_Project1Id");
            AddForeignKey("dbo.CuttingSheets", "Project1_Project1Id", "dbo.Project1", "Project1Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project1", "SalesTypeId", "dbo.SalesTypes");
            DropForeignKey("dbo.Project1", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.Project1", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Project1", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.CuttingSheets", "Project1_Project1Id", "dbo.Project1");
            DropForeignKey("dbo.Project1", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Project1", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Project1", new[] { "ProjectPaymentTermId" });
            DropIndex("dbo.Project1", new[] { "DesignerId" });
            DropIndex("dbo.Project1", new[] { "CustomerId" });
            DropIndex("dbo.Project1", new[] { "CustomerTypeId" });
            DropIndex("dbo.Project1", new[] { "SalesTypeId" });
            DropIndex("dbo.Project1", new[] { "SalesManId" });
            DropIndex("dbo.CuttingSheets", new[] { "Project1_Project1Id" });
            DropColumn("dbo.CuttingSheets", "Project1_Project1Id");
            DropTable("dbo.Project1");
        }
    }
}
