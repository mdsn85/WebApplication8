namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project2", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Project2", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Project2", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes");
            DropIndex("dbo.Project2", new[] { "SalesManId" });
            DropIndex("dbo.Project2", new[] { "SalesTypeId" });
            DropIndex("dbo.Project2", new[] { "CustomerTypeId" });
            DropIndex("dbo.Project2", new[] { "CustomerId" });
            DropIndex("dbo.Project2", new[] { "DesignerId" });
            DropIndex("dbo.Project2", new[] { "ProjectPaymentTermId" });
            DropColumn("dbo.Project2", "SalesManId");
            DropColumn("dbo.Project2", "SalesTypeId");
            DropColumn("dbo.Project2", "CustomerTypeId");
            DropColumn("dbo.Project2", "CustomerId");
            DropColumn("dbo.Project2", "DesignerId");
            DropColumn("dbo.Project2", "ProjectPaymentTermId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project2", "ProjectPaymentTermId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "DesignerId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "CustomerTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "SalesTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "SalesManId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "ProjectPaymentTermId");
            CreateIndex("dbo.Project2", "DesignerId");
            CreateIndex("dbo.Project2", "CustomerId");
            CreateIndex("dbo.Project2", "CustomerTypeId");
            CreateIndex("dbo.Project2", "SalesTypeId");
            CreateIndex("dbo.Project2", "SalesManId");
            AddForeignKey("dbo.Project2", "SalesTypeId", "dbo.SalesTypes", "SalesTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "SalesManId", "dbo.SalesMen", "SalesManId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
