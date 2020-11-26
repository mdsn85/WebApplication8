namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CuttingSheets", name: "Project2_Project2Id", newName: "Project2Id");
            RenameIndex(table: "dbo.CuttingSheets", name: "IX_Project2_Project2Id", newName: "IX_Project2Id");
            AddColumn("dbo.Projects", "CustomerTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "DesignerId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Value", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Discount", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Vat", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Total", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "SalePrice", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "ProjectPaymentTermId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Description", c => c.String());
            AddColumn("dbo.Projects", "DeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "ActualDeliveryDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Projects", "CustomerTypeId");
            CreateIndex("dbo.Projects", "CustomerId");
            CreateIndex("dbo.Projects", "DesignerId");
            CreateIndex("dbo.Projects", "ProjectPaymentTermId");
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes", "CustomerTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Projects", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Projects", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Projects", new[] { "ProjectPaymentTermId" });
            DropIndex("dbo.Projects", new[] { "DesignerId" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.Projects", new[] { "CustomerTypeId" });
            DropColumn("dbo.Projects", "ActualDeliveryDate");
            DropColumn("dbo.Projects", "DeliveryDate");
            DropColumn("dbo.Projects", "Description");
            DropColumn("dbo.Projects", "ProjectPaymentTermId");
            DropColumn("dbo.Projects", "SalePrice");
            DropColumn("dbo.Projects", "Total");
            DropColumn("dbo.Projects", "Vat");
            DropColumn("dbo.Projects", "Discount");
            DropColumn("dbo.Projects", "Value");
            DropColumn("dbo.Projects", "DesignerId");
            DropColumn("dbo.Projects", "CustomerId");
            DropColumn("dbo.Projects", "CustomerTypeId");
            RenameIndex(table: "dbo.CuttingSheets", name: "IX_Project2Id", newName: "IX_Project2_Project2Id");
            RenameColumn(table: "dbo.CuttingSheets", name: "Project2Id", newName: "Project2_Project2Id");
        }
    }
}
