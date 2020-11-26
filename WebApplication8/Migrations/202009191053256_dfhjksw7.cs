namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes");
            DropIndex("dbo.Project2", new[] { "SalesTypeId" });
            DropColumn("dbo.Project2", "SalesTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project2", "SalesTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "SalesTypeId");
            AddForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
        }
    }
}
