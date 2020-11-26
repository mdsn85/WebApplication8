namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project2", "CustomerTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "CustomerTypeId");
            AddForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes");
            DropIndex("dbo.Project2", new[] { "CustomerTypeId" });
            DropColumn("dbo.Project2", "CustomerTypeId");
        }
    }
}
