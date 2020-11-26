namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djdjpsdf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LpoDetails", "UnitId", "dbo.Units");
            DropIndex("dbo.LpoDetails", new[] { "UnitId" });
            DropColumn("dbo.LpoDetails", "UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LpoDetails", "UnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.LpoDetails", "UnitId");
            AddForeignKey("dbo.LpoDetails", "UnitId", "dbo.Units", "UnitId", cascadeDelete: true);
        }
    }
}
