namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project2", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Project2", new[] { "CustomerId" });
            DropColumn("dbo.Project2", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project2", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "CustomerId");
            AddForeignKey("dbo.Project2", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
