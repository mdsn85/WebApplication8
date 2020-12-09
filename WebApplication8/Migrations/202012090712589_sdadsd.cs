namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdadsd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CuttingSheetDetails", "IssueQty", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CuttingSheetDetails", "IssueQty", c => c.Double(nullable: false));
        }
    }
}
