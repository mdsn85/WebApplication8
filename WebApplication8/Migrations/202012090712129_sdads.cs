namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdads : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CuttingSheetDetails", "IssueQty", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CuttingSheetDetails", "IssueQty", c => c.Double());
        }
    }
}
