namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djdjp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditTerms",
                c => new
                    {
                        CreditTermId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CreditTermId);
            
            CreateTable(
                "dbo.Lpoes",
                c => new
                    {
                        LpoId = c.Int(nullable: false, identity: true),
                        code = c.Int(nullable: false),
                        SupplierRef = c.String(),
                        LpoDate = c.DateTime(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CreditTermId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.LpoId)
                .ForeignKey("dbo.CreditTerms", t => t.CreditTermId, cascadeDelete: true)
                .ForeignKey("dbo.suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.CreditTermId);
            
            CreateTable(
                "dbo.LpoDetails",
                c => new
                    {
                        LpoDetailId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        Qty = c.Single(nullable: false),
                        UnitId = c.Int(nullable: true),
                        Price = c.Single(nullable: false),
                        LpoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LpoDetailId)
                .ForeignKey("dbo.Lpoes", t => t.LpoId, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: false)
                .Index(t => t.MaterialId)
                .Index(t => t.UnitId)
                .Index(t => t.LpoId);
            
            CreateTable(
                "dbo.suppliers",
                c => new
                    {
                        supplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Attention = c.String(),
                        tel = c.String(),
                        mobile = c.String(),
                        email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.supplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lpoes", "SupplierId", "dbo.suppliers");
            DropForeignKey("dbo.LpoDetails", "UnitId", "dbo.Units");
            DropForeignKey("dbo.LpoDetails", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.LpoDetails", "LpoId", "dbo.Lpoes");
            DropForeignKey("dbo.Lpoes", "CreditTermId", "dbo.CreditTerms");
            DropIndex("dbo.LpoDetails", new[] { "LpoId" });
            DropIndex("dbo.LpoDetails", new[] { "UnitId" });
            DropIndex("dbo.LpoDetails", new[] { "MaterialId" });
            DropIndex("dbo.Lpoes", new[] { "CreditTermId" });
            DropIndex("dbo.Lpoes", new[] { "SupplierId" });
            DropTable("dbo.suppliers");
            DropTable("dbo.LpoDetails");
            DropTable("dbo.Lpoes");
            DropTable("dbo.CreditTerms");
        }
    }
}
