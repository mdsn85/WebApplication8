namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockIssueSequenses",
                c => new
                    {
                        StockIssueSequenseId = c.Int(nullable: false, identity: true),
                        Sequense = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockIssueSequenseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockIssueSequenses");
        }
    }
}
