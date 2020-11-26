namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project2", "SalesManId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "SalesManId");
            AddForeignKey("dbo.Project2", "SalesManId", "dbo.SalesMen", "SalesManId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project2", "SalesManId", "dbo.SalesMen");
            DropIndex("dbo.Project2", new[] { "SalesManId" });
            DropColumn("dbo.Project2", "SalesManId");
        }
    }
}
