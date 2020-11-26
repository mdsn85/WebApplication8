namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hhh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestApprovals",
                c => new
                    {
                        RequestApprovalId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        RequestTypeId = c.Int(nullable: false),
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
                        SalesManagerDate = c.String(),
                        ProductionDate = c.String(),
                        BDMDate = c.String(),
                        CoordinatorFullDate = c.String(),
                        CoordinatorPartialDate = c.String(),
                        CEODate = c.String(),
                        ApprovalType_ApprovalTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.RequestApprovalId)
                .ForeignKey("dbo.ApprovalTypes", t => t.ApprovalType_ApprovalTypeId)
                .ForeignKey("dbo.CuttingSheets", t => t.CuttingSheetId)
                .Index(t => t.CuttingSheetId)
                .Index(t => t.ApprovalType_ApprovalTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestApprovals", "CuttingSheetId", "dbo.CuttingSheets");
            DropForeignKey("dbo.RequestApprovals", "ApprovalType_ApprovalTypeId", "dbo.ApprovalTypes");
            DropIndex("dbo.RequestApprovals", new[] { "ApprovalType_ApprovalTypeId" });
            DropIndex("dbo.RequestApprovals", new[] { "CuttingSheetId" });
            DropTable("dbo.RequestApprovals");
        }
    }
}
