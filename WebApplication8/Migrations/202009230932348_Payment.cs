namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        Receipt = c.String(),
                        ModOfPayment = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
                        CollectedBy = c.String(),
                        CollectionDate = c.DateTime(nullable: false),
                        ChqNo = c.String(),
                        ChqDate = c.DateTime(),
                        TransBy = c.String(),
                        DateOfCredit = c.DateTime(nullable: false),
                        ToBankHM = c.String(),
                        ACHM = c.String(),
                        UserCreate = c.String(),
                        CreateDate = c.DateTime(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Projects", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Projects", "UserCreate", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Payments", new[] { "ProjectId" });
            DropColumn("dbo.Projects", "UserCreate");
            DropColumn("dbo.Projects", "CreateDate");
            DropTable("dbo.Payments");
        }
    }
}
