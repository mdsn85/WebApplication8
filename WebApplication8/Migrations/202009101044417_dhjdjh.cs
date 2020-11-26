namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhjdjh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materials", "Unit2_UnitId", "dbo.Units");
            DropForeignKey("dbo.Materials", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.Materials", new[] { "Unit_UnitId" });
            DropIndex("dbo.Materials", new[] { "Unit2_UnitId" });
            DropColumn("dbo.Materials", "UnitId");
            RenameColumn(table: "dbo.Materials", name: "Unit_UnitId", newName: "UnitId");
            AlterColumn("dbo.Materials", "UnitId", c => c.Int(nullable: true));
            CreateIndex("dbo.Materials", "UnitId");
            AddForeignKey("dbo.Materials", "UnitId", "dbo.Units", "UnitId", cascadeDelete: true);
            DropColumn("dbo.Materials", "Unit2_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materials", "Unit2_UnitId", c => c.Int());
            DropForeignKey("dbo.Materials", "UnitId", "dbo.Units");
            DropIndex("dbo.Materials", new[] { "UnitId" });
            AlterColumn("dbo.Materials", "UnitId", c => c.Int());
            RenameColumn(table: "dbo.Materials", name: "UnitId", newName: "Unit_UnitId");
            AddColumn("dbo.Materials", "UnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.Materials", "Unit2_UnitId");
            CreateIndex("dbo.Materials", "Unit_UnitId");
            AddForeignKey("dbo.Materials", "Unit_UnitId", "dbo.Units", "UnitId");
            AddForeignKey("dbo.Materials", "Unit2_UnitId", "dbo.Units", "UnitId");
        }
    }
}
