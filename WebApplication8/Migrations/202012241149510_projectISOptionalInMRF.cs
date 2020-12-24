namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectISOptionalInMRF : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CuttingSheets", "ProjectId", "dbo.Projects");
            DropIndex("dbo.CuttingSheets", new[] { "ProjectId" });
            AlterColumn("dbo.CuttingSheets", "ProjectId", c => c.Int());
            CreateIndex("dbo.CuttingSheets", "ProjectId");
            AddForeignKey("dbo.CuttingSheets", "ProjectId", "dbo.Projects", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CuttingSheets", "ProjectId", "dbo.Projects");
            DropIndex("dbo.CuttingSheets", new[] { "ProjectId" });
            AlterColumn("dbo.CuttingSheets", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.CuttingSheets", "ProjectId");
            AddForeignKey("dbo.CuttingSheets", "ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
        }
    }
}
