namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddj1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CuttingSheetDetails", "BalanceQty", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CuttingSheetDetails", "BalanceQty", c => c.Single());
        }
    }
}
