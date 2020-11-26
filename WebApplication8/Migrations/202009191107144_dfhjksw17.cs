namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes");
            DropIndex("dbo.Projects", new[] { "SalesTypeId" });
            DropIndex("dbo.Projects", new[] { "CustomerTypeId" });
            DropColumn("dbo.Projects", "SalesTypeId");
            DropColumn("dbo.Projects", "CustomerTypeId");
            DropTable("dbo.CustomerTypes");
            DropTable("dbo.SalesTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SalesTypes",
                c => new
                    {
                        SalesTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SalesTypeId);
            
            CreateTable(
                "dbo.CustomerTypes",
                c => new
                    {
                        CustomerTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CustomerTypeId);
            
            AddColumn("dbo.Projects", "CustomerTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "SalesTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "CustomerTypeId");
            CreateIndex("dbo.Projects", "SalesTypeId");
            AddForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
        }
    }
}
