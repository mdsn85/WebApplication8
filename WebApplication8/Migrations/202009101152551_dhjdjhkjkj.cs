namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhjdjhkjkj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Materials", "MinReOrder", c => c.Int());
            AlterColumn("dbo.Materials", "qty", c => c.Single());
            AlterColumn("dbo.Materials", "AvgPrice", c => c.Single());
            AlterColumn("dbo.Materials", "latestPrice", c => c.Single());
            AlterColumn("dbo.Materials", "UnitId2", c => c.Int());
            AlterColumn("dbo.Materials", "convert", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Materials", "convert", c => c.Single(nullable: false));
            AlterColumn("dbo.Materials", "UnitId2", c => c.Int(nullable: false));
            AlterColumn("dbo.Materials", "latestPrice", c => c.Single(nullable: false));
            AlterColumn("dbo.Materials", "AvgPrice", c => c.Single(nullable: false));
            AlterColumn("dbo.Materials", "qty", c => c.Single(nullable: false));
            AlterColumn("dbo.Materials", "MinReOrder", c => c.Int(nullable: false));
        }
    }
}
