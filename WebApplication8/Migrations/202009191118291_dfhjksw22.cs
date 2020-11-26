namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes");
            DropIndex("dbo.Projects", new[] { "SalesTypeId" });
            DropColumn("dbo.Projects", "SalesTypeId");
            DropTable("dbo.SalesTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SalesTypes",
                c => new
                    {
                        SalesTypeId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesTypeId);
            
            AddColumn("dbo.Projects", "SalesTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "SalesTypeId");
            AddForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
        }
    }
}
