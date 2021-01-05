namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRemarksToLPO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lpoes", "Remarks", c => c.String(maxLength: 200));
            AlterColumn("dbo.Lpoes", "code", c => c.String(maxLength: 15));
            AlterColumn("dbo.Lpoes", "UserCreate", c => c.String(maxLength: 20));
            AlterColumn("dbo.Lpoes", "QuotationRef", c => c.String(maxLength: 20));
            AlterColumn("dbo.Lpoes", "SupplierRef", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lpoes", "SupplierRef", c => c.String());
            AlterColumn("dbo.Lpoes", "QuotationRef", c => c.String());
            AlterColumn("dbo.Lpoes", "UserCreate", c => c.String());
            AlterColumn("dbo.Lpoes", "code", c => c.String());
            DropColumn("dbo.Lpoes", "Remarks");
        }
    }
}
