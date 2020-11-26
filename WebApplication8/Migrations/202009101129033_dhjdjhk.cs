namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhjdjhk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialUnits",
                c => new
                    {
                        MaterialUnitId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                        convert = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialUnitId)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: false)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: false)
                .Index(t => t.MaterialId)
                .Index(t => t.UnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialUnits", "UnitId", "dbo.Units");
            DropForeignKey("dbo.MaterialUnits", "MaterialId", "dbo.Materials");
            DropIndex("dbo.MaterialUnits", new[] { "UnitId" });
            DropIndex("dbo.MaterialUnits", new[] { "MaterialId" });
            DropTable("dbo.MaterialUnits");
        }
    }
}
