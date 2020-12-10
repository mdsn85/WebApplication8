namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djdjd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lpoes", "CreditTermId", "dbo.CreditTerms");
            DropIndex("dbo.Lpoes", new[] { "CreditTermId" });
            AddColumn("dbo.Lpoes", "CreditTerm_CreditTermSupplierId", c => c.Int());
            CreateIndex("dbo.Lpoes", "CreditTerm_CreditTermSupplierId");
            AddForeignKey("dbo.Lpoes", "CreditTerm_CreditTermSupplierId", "dbo.CreditTermSuppliers", "CreditTermSupplierId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lpoes", "CreditTerm_CreditTermSupplierId", "dbo.CreditTermSuppliers");
            DropIndex("dbo.Lpoes", new[] { "CreditTerm_CreditTermSupplierId" });
            DropColumn("dbo.Lpoes", "CreditTerm_CreditTermSupplierId");
            CreateIndex("dbo.Lpoes", "CreditTermId");
            AddForeignKey("dbo.Lpoes", "CreditTermId", "dbo.CreditTerms", "CreditTermId", cascadeDelete: true);
        }
    }
}
