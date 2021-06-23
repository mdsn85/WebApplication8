namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lead : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leads",
                c => new
                    {
                        LeadId = c.Int(nullable: false, identity: true),
                        DateRecieve = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 100),
                        ContactPerson = c.String(maxLength: 100),
                        Landline = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        AreaId = c.Int(nullable: false),
                        Address = c.String(maxLength: 300),
                        NatureOfBusinessId = c.Int(nullable: false),
                        description = c.String(maxLength: 500),
                        Remarks = c.String(maxLength: 500),
                        SalesManId = c.Int(nullable: false),
                        LastMeetingDate = c.DateTime(nullable: false),
                        ClosuerMonth = c.Int(nullable: false),
                        RemarksSalesman = c.String(),
                        WhoCreated = c.String(),
                        DateOfCreate = c.DateTime(nullable: false),
                        StatusOFLeadId = c.Int(nullable: false),
                        ModeOfLeadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeadId)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.ModeOfLeads", t => t.ModeOfLeadId, cascadeDelete: true)
                .ForeignKey("dbo.NatureOfBusinesses", t => t.NatureOfBusinessId, cascadeDelete: true)
                .ForeignKey("dbo.SalesMen", t => t.SalesManId, cascadeDelete: true)
                .ForeignKey("dbo.StatusOFLeads", t => t.StatusOFLeadId, cascadeDelete: true)
                .Index(t => t.AreaId)
                .Index(t => t.NatureOfBusinessId)
                .Index(t => t.SalesManId)
                .Index(t => t.StatusOFLeadId)
                .Index(t => t.ModeOfLeadId);
            
            CreateTable(
                "dbo.LeadFiles",
                c => new
                    {
                        LeadFileId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        ValidUntil = c.DateTime(),
                        LeadId = c.Int(),
                    })
                .PrimaryKey(t => t.LeadFileId)
                .ForeignKey("dbo.Leads", t => t.LeadId)
                .Index(t => t.LeadId);
            
            CreateTable(
                "dbo.ModeOfLeads",
                c => new
                    {
                        ModeOfLeadId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ModeOfLeadId);
            
            CreateTable(
                "dbo.NatureOfBusinesses",
                c => new
                    {
                        NatureOfBusinessId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.NatureOfBusinessId);
            
            CreateTable(
                "dbo.StatusOFLeads",
                c => new
                    {
                        StatusOFLeadId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StatusOFLeadId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leads", "StatusOFLeadId", "dbo.StatusOFLeads");
            DropForeignKey("dbo.Leads", "SalesManId", "dbo.SalesMen");
            DropForeignKey("dbo.Leads", "NatureOfBusinessId", "dbo.NatureOfBusinesses");
            DropForeignKey("dbo.Leads", "ModeOfLeadId", "dbo.ModeOfLeads");
            DropForeignKey("dbo.LeadFiles", "LeadId", "dbo.Leads");
            DropForeignKey("dbo.Leads", "AreaId", "dbo.Areas");
            DropIndex("dbo.LeadFiles", new[] { "LeadId" });
            DropIndex("dbo.Leads", new[] { "ModeOfLeadId" });
            DropIndex("dbo.Leads", new[] { "StatusOFLeadId" });
            DropIndex("dbo.Leads", new[] { "SalesManId" });
            DropIndex("dbo.Leads", new[] { "NatureOfBusinessId" });
            DropIndex("dbo.Leads", new[] { "AreaId" });
            DropTable("dbo.StatusOFLeads");
            DropTable("dbo.NatureOfBusinesses");
            DropTable("dbo.ModeOfLeads");
            DropTable("dbo.LeadFiles");
            DropTable("dbo.Leads");
        }
    }
}
