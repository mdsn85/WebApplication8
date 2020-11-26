namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuttingSheets", "StampDate", c => c.DateTime());
            AddColumn("dbo.StockIssues", "StampDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockIssues", "StampDate");
            DropColumn("dbo.CuttingSheets", "StampDate");
        }
    }
}
