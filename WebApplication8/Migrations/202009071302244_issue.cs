namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockIssues",
                c => new
                    {
                        StockIssueId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Ref = c.String(),
                        UserCreate = c.String(),
                        IssueDate = c.DateTime(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.StockIssueId)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.WarehouseId);
            
            CreateTable(
                "dbo.StockIssueDetails",
                c => new
                    {
                        StockIssueDetailId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        InQty = c.Single(nullable: false),
                        OutQty = c.Single(nullable: false),
                        Price = c.Single(nullable: false),
                        StockIssueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockIssueDetailId)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: false)
                .ForeignKey("dbo.StockIssues", t => t.StockIssueId, cascadeDelete: false)
                .Index(t => t.MaterialId)
                .Index(t => t.StockIssueId);
            
            CreateTable(
                "dbo.StockIssueTypes",
                c => new
                    {
                        StockIssueTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(),
                    })
                .PrimaryKey(t => t.StockIssueTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockIssues", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.StockIssueDetails", "StockIssueId", "dbo.StockIssues");
            DropForeignKey("dbo.StockIssueDetails", "MaterialId", "dbo.Materials");
            DropIndex("dbo.StockIssueDetails", new[] { "StockIssueId" });
            DropIndex("dbo.StockIssueDetails", new[] { "MaterialId" });
            DropIndex("dbo.StockIssues", new[] { "WarehouseId" });
            DropTable("dbo.StockIssueTypes");
            DropTable("dbo.StockIssueDetails");
            DropTable("dbo.StockIssues");
        }
    }
}
