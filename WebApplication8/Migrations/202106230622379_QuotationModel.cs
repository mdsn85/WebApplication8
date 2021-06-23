namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuotationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuotaionProducts",
                c => new
                    {
                        QuotationId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Dimensions = c.String(maxLength: 10),
                        Thickness = c.String(maxLength: 10),
                        Details = c.String(maxLength: 300),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuotationId, t.ProductId })
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Quotations", t => t.QuotationId, cascadeDelete: true)
                .Index(t => t.QuotationId)
                .Index(t => t.ProductId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.QuotaionProductFiles",
                c => new
                    {
                        QuotaionProductFileId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        ProductId = c.Int(nullable: false),
                        QuotationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuotaionProductFileId)
                .ForeignKey("dbo.QuotaionProducts", t => new { t.QuotationId, t.ProductId }, cascadeDelete: true)
                .Index(t => new { t.QuotationId, t.ProductId });
            
            CreateTable(
                "dbo.BankAccountDetails",
                c => new
                    {
                        BankAccountDetailId = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        AccountName = c.String(),
                        IBAN = c.String(),
                        AccountNo = c.String(),
                        SwiftCode = c.String(),
                        RoutingCode = c.String(),
                        Branch = c.String(),
                        TRN = c.String(),
                    })
                .PrimaryKey(t => t.BankAccountDetailId);
            
            CreateTable(
                "dbo.QuotationTerms",
                c => new
                    {
                        QuotationTermsId = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        QuotationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuotationTermsId)
                .ForeignKey("dbo.Quotations", t => t.QuotationId, cascadeDelete: true)
                .Index(t => t.QuotationId);
            
            AddColumn("dbo.Quotations", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Quotations", "Vat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Quotations", "FinalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Quotations", "LeadId", c => c.Int());
            AddColumn("dbo.Quotations", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quotations", "QuotationRef", c => c.String());
            AddColumn("dbo.Quotations", "DesignerId", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "BankAccountDetailId", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "DeliveryAreaId", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "ProjectPaymentTermId", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "ModOfPayment", c => c.Int(nullable: false));
            AddColumn("dbo.Quotations", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Quotations", "UserCreate", c => c.String(maxLength: 20));
            AddColumn("dbo.Quotations", "StampDate", c => c.DateTime());
            AddColumn("dbo.Quotations", "LastUpdateDate", c => c.DateTime());
            AddColumn("dbo.Quotations", "DeliveryArea_AreaId", c => c.Int());
            CreateIndex("dbo.Quotations", "LeadId");
            CreateIndex("dbo.Quotations", "DesignerId");
            CreateIndex("dbo.Quotations", "BankAccountDetailId");
            CreateIndex("dbo.Quotations", "ProjectPaymentTermId");
            CreateIndex("dbo.Quotations", "DeliveryArea_AreaId");
            AddForeignKey("dbo.Quotations", "BankAccountDetailId", "dbo.BankAccountDetails", "BankAccountDetailId", cascadeDelete: true);
            AddForeignKey("dbo.Quotations", "DeliveryArea_AreaId", "dbo.Areas", "AreaId");
            AddForeignKey("dbo.Quotations", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
            AddForeignKey("dbo.Quotations", "LeadId", "dbo.Leads", "LeadId");
            AddForeignKey("dbo.Quotations", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotationTerms", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.QuotaionProducts", "QuotationId", "dbo.Quotations");
            DropForeignKey("dbo.Quotations", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropForeignKey("dbo.Quotations", "LeadId", "dbo.Leads");
            DropForeignKey("dbo.Quotations", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Quotations", "DeliveryArea_AreaId", "dbo.Areas");
            DropForeignKey("dbo.Quotations", "BankAccountDetailId", "dbo.BankAccountDetails");
            DropForeignKey("dbo.QuotaionProductFiles", new[] { "QuotationId", "ProductId" }, "dbo.QuotaionProducts");
            DropForeignKey("dbo.QuotaionProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.QuotaionProducts", "MaterialId", "dbo.Materials");
            DropIndex("dbo.QuotationTerms", new[] { "QuotationId" });
            DropIndex("dbo.Quotations", new[] { "DeliveryArea_AreaId" });
            DropIndex("dbo.Quotations", new[] { "ProjectPaymentTermId" });
            DropIndex("dbo.Quotations", new[] { "BankAccountDetailId" });
            DropIndex("dbo.Quotations", new[] { "DesignerId" });
            DropIndex("dbo.Quotations", new[] { "LeadId" });
            DropIndex("dbo.QuotaionProductFiles", new[] { "QuotationId", "ProductId" });
            DropIndex("dbo.QuotaionProducts", new[] { "MaterialId" });
            DropIndex("dbo.QuotaionProducts", new[] { "ProductId" });
            DropIndex("dbo.QuotaionProducts", new[] { "QuotationId" });
            DropColumn("dbo.Quotations", "DeliveryArea_AreaId");
            DropColumn("dbo.Quotations", "LastUpdateDate");
            DropColumn("dbo.Quotations", "StampDate");
            DropColumn("dbo.Quotations", "UserCreate");
            DropColumn("dbo.Quotations", "CreateDate");
            DropColumn("dbo.Quotations", "ModOfPayment");
            DropColumn("dbo.Quotations", "ProjectPaymentTermId");
            DropColumn("dbo.Quotations", "DeliveryAreaId");
            DropColumn("dbo.Quotations", "BankAccountDetailId");
            DropColumn("dbo.Quotations", "DesignerId");
            DropColumn("dbo.Quotations", "QuotationRef");
            DropColumn("dbo.Quotations", "Date");
            DropColumn("dbo.Quotations", "LeadId");
            DropColumn("dbo.Quotations", "FinalPrice");
            DropColumn("dbo.Quotations", "Vat");
            DropColumn("dbo.Quotations", "Total");
            DropTable("dbo.QuotationTerms");
            DropTable("dbo.BankAccountDetails");
            DropTable("dbo.QuotaionProductFiles");
            DropTable("dbo.QuotaionProducts");
        }
    }
}
