namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fhfwk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockIssues", "LpoId", c => c.Int());
            AddColumn("dbo.StockIssues", "CuttingSheetId", c => c.Int());
            CreateIndex("dbo.StockIssues", "LpoId");
            CreateIndex("dbo.StockIssues", "CuttingSheetId");
            AddForeignKey("dbo.StockIssues", "CuttingSheetId", "dbo.CuttingSheets", "CuttingSheetId");
            AddForeignKey("dbo.StockIssues", "LpoId", "dbo.Lpoes", "LpoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockIssues", "LpoId", "dbo.Lpoes");
            DropForeignKey("dbo.StockIssues", "CuttingSheetId", "dbo.CuttingSheets");
            DropIndex("dbo.StockIssues", new[] { "CuttingSheetId" });
            DropIndex("dbo.StockIssues", new[] { "LpoId" });
            DropColumn("dbo.StockIssues", "CuttingSheetId");
            DropColumn("dbo.StockIssues", "LpoId");
        }
    }
}
