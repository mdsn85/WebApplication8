namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes");
            DropIndex("dbo.Project2", new[] { "CustomerTypeId" });
            AddColumn("dbo.Project2", "SalesTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "SalesTypeId");
            AddForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
            DropColumn("dbo.Project2", "CustomerTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project2", "CustomerTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes");
            DropIndex("dbo.Project2", new[] { "SalesTypeId" });
            DropColumn("dbo.Project2", "SalesTypeId");
            CreateIndex("dbo.Project2", "CustomerTypeId");
            AddForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
        }
    }
}
