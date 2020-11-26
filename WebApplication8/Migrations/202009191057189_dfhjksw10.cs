namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes");
            DropIndex("dbo.Project2", new[] { "CustomerTypeId" });
            DropColumn("dbo.Project2", "CustomerTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project2", "CustomerTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "CustomerTypeId");
            AddForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
        }
    }
}
