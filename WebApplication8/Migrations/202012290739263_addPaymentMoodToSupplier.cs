namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPaymentMoodToSupplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentMoodSuppliers",
                c => new
                    {
                        PaymentMoodSupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PaymentMoodSupplierId);
            
            AddColumn("dbo.suppliers", "PaymentMoodSupplierId", c => c.Int());
            CreateIndex("dbo.suppliers", "PaymentMoodSupplierId");
            AddForeignKey("dbo.suppliers", "PaymentMoodSupplierId", "dbo.PaymentMoodSuppliers", "PaymentMoodSupplierId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.suppliers", "PaymentMoodSupplierId", "dbo.PaymentMoodSuppliers");
            DropIndex("dbo.suppliers", new[] { "PaymentMoodSupplierId" });
            DropColumn("dbo.suppliers", "PaymentMoodSupplierId");
            DropTable("dbo.PaymentMoodSuppliers");
        }
    }
}
