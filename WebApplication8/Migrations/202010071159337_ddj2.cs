namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddj2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuttingSheetDetails", "IssueQty", c => c.Double(nullable: false, defaultValue: 0));
            DropColumn("dbo.CuttingSheetDetails", "BalanceQty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CuttingSheetDetails", "BalanceQty", c => c.Double());
            DropColumn("dbo.CuttingSheetDetails", "IssueQty");
        }
    }
}
