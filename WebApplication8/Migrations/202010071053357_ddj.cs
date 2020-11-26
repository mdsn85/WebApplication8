namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuttingSheetDetails", "BalanceQty", c => c.Double(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CuttingSheetDetails", "BalanceQty");
        }
    }
}
