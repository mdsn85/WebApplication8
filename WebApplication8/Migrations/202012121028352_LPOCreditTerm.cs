namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LPOCreditTerm : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lpoes", name: "CreditTerm_CreditTermSupplierId", newName: "CreditTermSupplier_CreditTermSupplierId");
            RenameIndex(table: "dbo.Lpoes", name: "IX_CreditTerm_CreditTermSupplierId", newName: "IX_CreditTermSupplier_CreditTermSupplierId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Lpoes", name: "IX_CreditTermSupplier_CreditTermSupplierId", newName: "IX_CreditTerm_CreditTermSupplierId");
            RenameColumn(table: "dbo.Lpoes", name: "CreditTermSupplier_CreditTermSupplierId", newName: "CreditTerm_CreditTermSupplierId");
        }
    }
}
