namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhflds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Attention = c.String(),
                        tel = c.String(),
                        mobile = c.String(),
                        email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.SalesMen",
                c => new
                    {
                        SalesManId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.SalesManId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            AddColumn("dbo.Projects", "SalesDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "SalesManId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Value", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Discount", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Vat", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "Total", c => c.Single(nullable: false));
            AddColumn("dbo.Projects", "DeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "ActualDeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "AccountApproval", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Projects", "SalesManId");
            CreateIndex("dbo.Projects", "CustomerId");
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "SalesManId", "dbo.SalesMen", "SalesManId", cascadeDelete: true);
            DropColumn("dbo.Projects", "ProjectDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "ProjectDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Projects", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.SalesMen", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SalesMen", new[] { "EmployeeId" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.Projects", new[] { "SalesManId" });
            DropColumn("dbo.Projects", "AccountApproval");
            DropColumn("dbo.Projects", "ActualDeliveryDate");
            DropColumn("dbo.Projects", "DeliveryDate");
            DropColumn("dbo.Projects", "Total");
            DropColumn("dbo.Projects", "Vat");
            DropColumn("dbo.Projects", "Discount");
            DropColumn("dbo.Projects", "Value");
            DropColumn("dbo.Projects", "CustomerId");
            DropColumn("dbo.Projects", "SalesManId");
            DropColumn("dbo.Projects", "SalesDate");
            DropTable("dbo.SalesMen");
            DropTable("dbo.Customers");
        }
    }
}
