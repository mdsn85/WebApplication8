namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CuttingSheetDetails",
                c => new
                    {
                        CuttingSheetDetailId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        qty = c.Single(nullable: false),
                        CuttingSheetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CuttingSheetDetailId)
                .ForeignKey("dbo.CuttingSheets", t => t.CuttingSheetId, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.CuttingSheetId);
            
            CreateTable(
                "dbo.CuttingSheets",
                c => new
                    {
                        CuttingSheetId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserCreate = c.String(),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CuttingSheetId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProjectDate = c.DateTime(nullable: false),
                        SalePrice = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MinReOrder = c.Int(nullable: false),
                        Description = c.String(),
                        qty = c.Single(nullable: false),
                        AvgPrice = c.Single(nullable: false),
                        UnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CuttingSheetDetails", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Materials", "UnitId", "dbo.Units");
            DropForeignKey("dbo.CuttingSheets", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CuttingSheetDetails", "CuttingSheetId", "dbo.CuttingSheets");
            DropIndex("dbo.Materials", new[] { "UnitId" });
            DropIndex("dbo.CuttingSheets", new[] { "ProjectId" });
            DropIndex("dbo.CuttingSheetDetails", new[] { "CuttingSheetId" });
            DropIndex("dbo.CuttingSheetDetails", new[] { "MaterialId" });
            DropTable("dbo.Units");
            DropTable("dbo.Materials");
            DropTable("dbo.Projects");
            DropTable("dbo.CuttingSheets");
            DropTable("dbo.CuttingSheetDetails");
        }
    }
}
