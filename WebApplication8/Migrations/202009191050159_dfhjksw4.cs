namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project2", "SalesTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "CustomerTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "SalesTypeId");
            CreateIndex("dbo.Project2", "CustomerTypeId");
            CreateIndex("dbo.Project2", "CustomerId");
            AddForeignKey("dbo.Project2", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes");
            DropForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Project2", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Project2", new[] { "CustomerId" });
            DropIndex("dbo.Project2", new[] { "CustomerTypeId" });
            DropIndex("dbo.Project2", new[] { "SalesTypeId" });
            DropColumn("dbo.Project2", "CustomerId");
            DropColumn("dbo.Project2", "CustomerTypeId");
            DropColumn("dbo.Project2", "SalesTypeId");
        }
    }
}
