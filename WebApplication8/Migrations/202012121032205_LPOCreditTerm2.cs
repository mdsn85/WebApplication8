namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LPOCreditTerm2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lpoes", name: "CreditTermSupplier_CreditTermSupplierId", newName: "CreditTermSupplierId");
            RenameIndex(table: "dbo.Lpoes", name: "IX_CreditTermSupplier_CreditTermSupplierId", newName: "IX_CreditTermSupplierId");
            DropColumn("dbo.Lpoes", "CreditTermId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lpoes", "CreditTermId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Lpoes", name: "IX_CreditTermSupplierId", newName: "IX_CreditTermSupplier_CreditTermSupplierId");
            RenameColumn(table: "dbo.Lpoes", name: "CreditTermSupplierId", newName: "CreditTermSupplier_CreditTermSupplierId");
        }
    }
}
