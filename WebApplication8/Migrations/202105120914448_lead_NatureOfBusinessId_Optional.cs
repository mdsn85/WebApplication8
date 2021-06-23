namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lead_NatureOfBusinessId_Optional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leads", "NatureOfBusinessId", "dbo.NatureOfBusinesses");
            DropIndex("dbo.Leads", new[] { "NatureOfBusinessId" });
            AlterColumn("dbo.Leads", "NatureOfBusinessId", c => c.Int());
            CreateIndex("dbo.Leads", "NatureOfBusinessId");
            AddForeignKey("dbo.Leads", "NatureOfBusinessId", "dbo.NatureOfBusinesses", "NatureOfBusinessId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leads", "NatureOfBusinessId", "dbo.NatureOfBusinesses");
            DropIndex("dbo.Leads", new[] { "NatureOfBusinessId" });
            AlterColumn("dbo.Leads", "NatureOfBusinessId", c => c.Int(nullable: false));
            CreateIndex("dbo.Leads", "NatureOfBusinessId");
            AddForeignKey("dbo.Leads", "NatureOfBusinessId", "dbo.NatureOfBusinesses", "NatureOfBusinessId", cascadeDelete: true);
        }
    }
}
