namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hl : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RequestApprovalTypes",
                c => new
                    {
                        RequestApprovalTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RequestApprovalTypeId);
            
            CreateTable(
                "dbo.RequestApprovals",
                c => new
                    {
                        RequestApprovalId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        RequestApprovalTypeId = c.Int(nullable: false),
                        CuttingSheetId = c.Int(),
                        details = c.String(),
                        UserName = c.String(),
                        ProductionManager = c.Boolean(nullable: false),
                        SalesManager = c.Boolean(nullable: false),
                        BDM = c.Boolean(nullable: false),
                        CoordinatorFull = c.Boolean(nullable: false),
                        CoordinatorPartial = c.Boolean(nullable: false),
                        CEO = c.Boolean(nullable: false),
                        IsRejected = c.Boolean(),
                        ProductionManagerRemarks = c.String(),
                        SalesManagerRemarks = c.String(),
                        BDMRemarks = c.String(),
                        CoordinatorFullRemarks = c.String(),
                        CoordinatorPartialRemarks = c.String(),
                        CEORemarks = c.String(),
                        ProductionManagerDate = c.String(),
                        SalesManagerDate = c.String(),
                        BDMDate = c.String(),
                        CoordinatorFullDate = c.String(),
                        CoordinatorPartialDate = c.String(),
                        CEODate = c.String(),
                    })
                .PrimaryKey(t => t.RequestApprovalId);
            
            CreateIndex("dbo.RequestApprovals", "CuttingSheetId");
            CreateIndex("dbo.RequestApprovals", "RequestApprovalTypeId");
            AddForeignKey("dbo.RequestApprovals", "RequestApprovalTypeId", "dbo.RequestApprovalTypes", "RequestApprovalTypeId", cascadeDelete: true);
            AddForeignKey("dbo.RequestApprovals", "CuttingSheetId", "dbo.CuttingSheets", "CuttingSheetId");
        }
    }
}
