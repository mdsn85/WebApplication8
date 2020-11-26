namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdjlak : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materials", "UnitId", "dbo.Units");
            DropIndex("dbo.Materials", new[] { "UnitId" });
            AddColumn("dbo.Materials", "UnitId2", c => c.Int(nullable: false));
            AddColumn("dbo.Materials", "convert", c => c.Single(nullable: false));
            AddColumn("dbo.Materials", "Unit_UnitId", c => c.Int());
            AddColumn("dbo.Materials", "Unit2_UnitId", c => c.Int());
            CreateIndex("dbo.Materials", "Unit_UnitId");
            CreateIndex("dbo.Materials", "Unit2_UnitId");
            AddForeignKey("dbo.Materials", "Unit2_UnitId", "dbo.Units", "UnitId");
            AddForeignKey("dbo.Materials", "Unit_UnitId", "dbo.Units", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materials", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.Materials", "Unit2_UnitId", "dbo.Units");
            DropIndex("dbo.Materials", new[] { "Unit2_UnitId" });
            DropIndex("dbo.Materials", new[] { "Unit_UnitId" });
            DropColumn("dbo.Materials", "Unit2_UnitId");
            DropColumn("dbo.Materials", "Unit_UnitId");
            DropColumn("dbo.Materials", "convert");
            DropColumn("dbo.Materials", "UnitId2");
            CreateIndex("dbo.Materials", "UnitId");
            AddForeignKey("dbo.Materials", "UnitId", "dbo.Units", "UnitId", cascadeDelete: true);
        }
    }
}
