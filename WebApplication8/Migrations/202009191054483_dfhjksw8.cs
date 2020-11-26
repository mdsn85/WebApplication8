namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project2", "CustomerTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "DesignerId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "ProjectPaymentTermId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "CustomerTypeId");
            CreateIndex("dbo.Project2", "CustomerId");
            CreateIndex("dbo.Project2", "DesignerId");
            CreateIndex("dbo.Project2", "ProjectPaymentTermId");
            AddForeignKey("dbo.Project2", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Project2", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Project2", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Project2", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Project2", new[] { "ProjectPaymentTermId" });
            DropIndex("dbo.Project2", new[] { "DesignerId" });
            DropIndex("dbo.Project2", new[] { "CustomerId" });
            DropIndex("dbo.Project2", new[] { "CustomerTypeId" });
            DropColumn("dbo.Project2", "ProjectPaymentTermId");
            DropColumn("dbo.Project2", "DesignerId");
            DropColumn("dbo.Project2", "CustomerId");
            DropColumn("dbo.Project2", "CustomerTypeId");
        }
    }
}
