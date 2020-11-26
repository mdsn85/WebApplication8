namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomersTypes",
                c => new
                    {
                        CustomersTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CustomersTypeId);
            
            CreateTable(
                "dbo.SalesTypes",
                c => new
                    {
                        SalesTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SalesTypeId);
            
            AddColumn("dbo.Projects", "SalesTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "CustomersTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "SalesTypeId");
            CreateIndex("dbo.Projects", "CustomersTypeId");
            AddForeignKey("dbo.Projects", "CustomersTypeId", "dbo.CustomersTypes", "CustomersTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes");
            DropForeignKey("dbo.Projects", "CustomersTypeId", "dbo.CustomersTypes");
            DropIndex("dbo.Projects", new[] { "CustomersTypeId" });
            DropIndex("dbo.Projects", new[] { "SalesTypeId" });
            DropColumn("dbo.Projects", "CustomersTypeId");
            DropColumn("dbo.Projects", "SalesTypeId");
            DropTable("dbo.SalesTypes");
            DropTable("dbo.CustomersTypes");
        }
    }
}
