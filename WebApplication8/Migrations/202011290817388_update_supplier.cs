namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_supplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditTermSuppliers",
                c => new
                    {
                        CreditTermSupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CreditTermSupplierId);
            
            AddColumn("dbo.suppliers", "CreditTermSupplierId", c => c.Int());
            CreateIndex("dbo.suppliers", "CreditTermSupplierId");
            AddForeignKey("dbo.suppliers", "CreditTermSupplierId", "dbo.CreditTermSuppliers", "CreditTermSupplierId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.suppliers", "CreditTermSupplierId", "dbo.CreditTermSuppliers");
            DropIndex("dbo.suppliers", new[] { "CreditTermSupplierId" });
            DropColumn("dbo.suppliers", "CreditTermSupplierId");
            DropTable("dbo.CreditTermSuppliers");
        }
    }
}
