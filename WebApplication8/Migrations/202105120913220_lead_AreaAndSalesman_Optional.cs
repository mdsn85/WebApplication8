namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lead_AreaAndSalesman_Optional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leads", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Leads", "SalesManId", "dbo.SalesMen");
            DropIndex("dbo.Leads", new[] { "AreaId" });
            DropIndex("dbo.Leads", new[] { "SalesManId" });
            AlterColumn("dbo.Leads", "AreaId", c => c.Int());
            AlterColumn("dbo.Leads", "SalesManId", c => c.Int());
            CreateIndex("dbo.Leads", "AreaId");
            CreateIndex("dbo.Leads", "SalesManId");
            AddForeignKey("dbo.Leads", "AreaId", "dbo.Areas", "AreaId");
            AddForeignKey("dbo.Leads", "SalesManId", "dbo.SalesMen", "SalesManId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leads", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.Leads", "AreaId", "dbo.Areas");
            DropIndex("dbo.Leads", new[] { "SalesManId" });
            DropIndex("dbo.Leads", new[] { "AreaId" });
            AlterColumn("dbo.Leads", "SalesManId", c => c.Int(nullable: false));
            AlterColumn("dbo.Leads", "AreaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Leads", "SalesManId");
            CreateIndex("dbo.Leads", "AreaId");
            AddForeignKey("dbo.Leads", "SalesManId", "dbo.SalesMen", "SalesManId", cascadeDelete: true);
            AddForeignKey("dbo.Leads", "AreaId", "dbo.Areas", "AreaId", cascadeDelete: true);
        }
    }
}
