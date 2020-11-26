namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hhhol : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestApprovals", "ApprovalType_ApprovalTypeId", "dbo.ApprovalTypes");
            DropIndex("dbo.RequestApprovals", new[] { "ApprovalType_ApprovalTypeId" });
            RenameColumn(table: "dbo.RequestApprovals", name: "ApprovalType_ApprovalTypeId", newName: "ApprovalTypeId");
            CreateTable(
                "dbo.SalesTeams",
                c => new
                    {
                        SalesTeamId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.SalesTeamId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmailServers",
                c => new
                    {
                        EmailServerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Host = c.String(),
                        EnableSsl = c.Boolean(nullable: false),
                        Port = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmailServerId);
            
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        QuotationId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        CustomerId = c.Int(),
                        SalesManId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuotationId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.SalesMen", t => t.SalesManId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.SalesManId);
            
            AddColumn("dbo.SalesMen", "SalesTeamId", c => c.Int());
            AddColumn("dbo.SalesMen", "isTeamleader", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestApprovals", "QuotationId", c => c.Int());
            AlterColumn("dbo.RequestApprovals", "ApprovalTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.SalesMen", "SalesTeamId");
            CreateIndex("dbo.RequestApprovals", "ApprovalTypeId");
            CreateIndex("dbo.RequestApprovals", "QuotationId");
            AddForeignKey("dbo.SalesMen", "SalesTeamId", "dbo.SalesTeams", "SalesTeamId");
            AddForeignKey("dbo.RequestApprovals", "QuotationId", "dbo.Quotations", "QuotationId");
            AddForeignKey("dbo.RequestApprovals", "ApprovalTypeId", "dbo.ApprovalTypes", "ApprovalTypeId", cascadeDelete: true);
            DropColumn("dbo.RequestApprovals", "RequestTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestApprovals", "RequestTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RequestApprovals", "ApprovalTypeId", "dbo.ApprovalTypes");
            DropForeignKey("dbo.RequestApprovals", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.Quotations", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.Quotations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SalesMen", "SalesTeamId", "dbo.SalesTeams");
            DropForeignKey("dbo.SalesTeams", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.RequestApprovals", new[] { "QuotationId" });
            DropIndex("dbo.RequestApprovals", new[] { "ApprovalTypeId" });
            DropIndex("dbo.Quotations", new[] { "SalesManId" });
            DropIndex("dbo.Quotations", new[] { "CustomerId" });
            DropIndex("dbo.SalesTeams", new[] { "EmployeeId" });
            DropIndex("dbo.SalesMen", new[] { "SalesTeamId" });
            AlterColumn("dbo.RequestApprovals", "ApprovalTypeId", c => c.Int());
            DropColumn("dbo.RequestApprovals", "QuotationId");
            DropColumn("dbo.SalesMen", "isTeamleader");
            DropColumn("dbo.SalesMen", "SalesTeamId");
            DropTable("dbo.Quotations");
            DropTable("dbo.EmailServers");
            DropTable("dbo.SalesTeams");
            RenameColumn(table: "dbo.RequestApprovals", name: "ApprovalTypeId", newName: "ApprovalType_ApprovalTypeId");
            CreateIndex("dbo.RequestApprovals", "ApprovalType_ApprovalTypeId");
            AddForeignKey("dbo.RequestApprovals", "ApprovalType_ApprovalTypeId", "dbo.ApprovalTypes", "ApprovalTypeId");
        }
    }
}
