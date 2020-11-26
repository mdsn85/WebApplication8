namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project1", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Project1", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.CuttingSheets", "Project1_Project1Id", "dbo.Project1");
            DropForeignKey("dbo.Project1", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Project1", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Project1", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.Project1", "SalesTypeId", "dbo.SalesTypes");
            DropIndex("dbo.CuttingSheets", new[] { "Project1_Project1Id" });
            DropIndex("dbo.Project1", new[] { "SalesManId" });
            DropIndex("dbo.Project1", new[] { "SalesTypeId" });
            DropIndex("dbo.Project1", new[] { "CustomerTypeId" });
            DropIndex("dbo.Project1", new[] { "CustomerId" });
            DropIndex("dbo.Project1", new[] { "DesignerId" });
            DropIndex("dbo.Project1", new[] { "ProjectPaymentTermId" });
            CreateTable(
                "dbo.Class1",
                c => new
                    {
                        Class1Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Class1Id);
            
            CreateTable(
                "dbo.Project2",
                c => new
                    {
                        Project2Id = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.Project2Id)
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
            
            AddColumn("dbo.CuttingSheets", "Project2_Project2Id", c => c.Int());
            CreateIndex("dbo.CuttingSheets", "Project2_Project2Id");
            AddForeignKey("dbo.CuttingSheets", "Project2_Project2Id", "dbo.Project2", "Project2Id");
            DropColumn("dbo.CuttingSheets", "Project1_Project1Id");
            DropTable("dbo.Project1");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Project1Id);
            
            AddColumn("dbo.CuttingSheets", "Project1_Project1Id", c => c.Int());
            DropForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes");
            DropForeignKey("dbo.Project2", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Project2", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.CuttingSheets", "Project2_Project2Id", "dbo.Project2");
            DropForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Project2", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Project2", new[] { "ProjectPaymentTermId" });
            DropIndex("dbo.Project2", new[] { "DesignerId" });
            DropIndex("dbo.Project2", new[] { "CustomerId" });
            DropIndex("dbo.Project2", new[] { "CustomerTypeId" });
            DropIndex("dbo.Project2", new[] { "SalesTypeId" });
            DropIndex("dbo.Project2", new[] { "SalesManId" });
            DropIndex("dbo.CuttingSheets", new[] { "Project2_Project2Id" });
            DropColumn("dbo.CuttingSheets", "Project2_Project2Id");
            DropTable("dbo.Project2");
            DropTable("dbo.Class1");
            CreateIndex("dbo.Project1", "ProjectPaymentTermId");
            CreateIndex("dbo.Project1", "DesignerId");
            CreateIndex("dbo.Project1", "CustomerId");
            CreateIndex("dbo.Project1", "CustomerTypeId");
            CreateIndex("dbo.Project1", "SalesTypeId");
            CreateIndex("dbo.Project1", "SalesManId");
            CreateIndex("dbo.CuttingSheets", "Project1_Project1Id");
            AddForeignKey("dbo.Project1", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Project1", "SalesManId", "dbo.SalesMen", "SalesManId", cascadeDelete: true);
            AddForeignKey("dbo.Project1", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
            AddForeignKey("dbo.Project1", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
            AddForeignKey("dbo.CuttingSheets", "Project1_Project1Id", "dbo.Project1", "Project1Id");
            AddForeignKey("dbo.Project1", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Project1", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
