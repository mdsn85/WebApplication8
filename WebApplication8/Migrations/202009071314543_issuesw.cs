namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issuesw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockIssues", "StockIssueTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.StockIssues", "StockIssueTypeId");
            AddForeignKey("dbo.StockIssues", "StockIssueTypeId", "dbo.StockIssueTypes", "StockIssueTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockIssues", "StockIssueTypeId", "dbo.StockIssueTypes");
            DropIndex("dbo.StockIssues", new[] { "StockIssueTypeId" });
            DropColumn("dbo.StockIssues", "StockIssueTypeId");
        }
    }
}
