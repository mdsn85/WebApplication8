namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project2", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Project2", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropIndex("dbo.Project2", new[] { "CustomerId" });
            DropIndex("dbo.Project2", new[] { "DesignerId" });
            DropIndex("dbo.Project2", new[] { "ProjectPaymentTermId" });
            DropColumn("dbo.Project2", "CustomerId");
            DropColumn("dbo.Project2", "DesignerId");
            DropColumn("dbo.Project2", "ProjectPaymentTermId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project2", "ProjectPaymentTermId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "DesignerId", c => c.Int(nullable: false));
            AddColumn("dbo.Project2", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "ProjectPaymentTermId");
            CreateIndex("dbo.Project2", "DesignerId");
            CreateIndex("dbo.Project2", "CustomerId");
            AddForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
            AddForeignKey("dbo.Project2", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
