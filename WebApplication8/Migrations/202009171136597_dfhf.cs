namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerTypes",
                c => new
                    {
                        CustomerTypeId = c.Int(nullable: true, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CustomerTypeId);
            
            CreateTable(
                "dbo.ProjectPaymentTerms",
                c => new
                    {
                        ProjectPaymentTermId = c.Int(nullable: true, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProjectPaymentTermId);
            
            CreateTable(
                "dbo.SalesTypes",
                c => new
                    {
                        SalesTypeId = c.Int(nullable: true, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SalesTypeId);
            
            AddColumn("dbo.Projects", "SalesTypeId", c => c.Int(nullable: true));
            AddColumn("dbo.Projects", "CustomerTypeId", c => c.Int(nullable: true));
            AddColumn("dbo.Projects", "ProjectPaymentTermId", c => c.Int(nullable: true));
            CreateIndex("dbo.Projects", "SalesTypeId");
            CreateIndex("dbo.Projects", "CustomerTypeId");
            CreateIndex("dbo.Projects", "ProjectPaymentTermId");
            AddForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "SalesTypeId", "dbo.SalesTypes");
            DropForeignKey("dbo.Projects", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes");
            DropIndex("dbo.Projects", new[] { "ProjectPaymentTermId" });
            DropIndex("dbo.Projects", new[] { "CustomerTypeId" });
            DropIndex("dbo.Projects", new[] { "SalesTypeId" });
            DropColumn("dbo.Projects", "ProjectPaymentTermId");
            DropColumn("dbo.Projects", "CustomerTypeId");
            DropColumn("dbo.Projects", "SalesTypeId");
            DropTable("dbo.SalesTypes");
            DropTable("dbo.ProjectPaymentTerms");
            DropTable("dbo.CustomerTypes");
        }
    }
}
