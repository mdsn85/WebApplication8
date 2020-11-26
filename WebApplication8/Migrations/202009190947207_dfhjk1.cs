namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjk1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Projects", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Projects", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropIndex("dbo.Projects", new[] { "CustomerTypeId" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.Projects", new[] { "DesignerId" });
            DropIndex("dbo.Projects", new[] { "ProjectPaymentTermId" });
            DropColumn("dbo.Projects", "CustomerTypeId");
            DropColumn("dbo.Projects", "CustomerId");
            DropColumn("dbo.Projects", "DesignerId");
            DropColumn("dbo.Projects", "Value");
            DropColumn("dbo.Projects", "Discount");
            DropColumn("dbo.Projects", "Vat");
            DropColumn("dbo.Projects", "Total");
            DropColumn("dbo.Projects", "SalePrice");
            DropColumn("dbo.Projects", "ProjectPaymentTermId");
            DropColumn("dbo.Projects", "Description");
            DropColumn("dbo.Projects", "DeliveryDate");
            DropColumn("dbo.Projects", "ActualDeliveryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "ActualDeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "DeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "Description", c => c.String());
            AddColumn("dbo.Projects", "ProjectPaymentTermId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "SalePrice", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Total", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Vat", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Discount", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Value", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "DesignerId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "CustomerTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "ProjectPaymentTermId");
            CreateIndex("dbo.Projects", "DesignerId");
            CreateIndex("dbo.Projects", "CustomerId");
            CreateIndex("dbo.Projects", "CustomerTypeId");
            AddForeignKey("dbo.Projects", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
