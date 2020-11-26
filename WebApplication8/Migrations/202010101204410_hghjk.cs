namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hghjk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuttingSheetDetails", "qtyApproved", c => c.Single(nullable: false));
            AddColumn("dbo.CuttingSheetDetails", "Approval", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CuttingSheetDetails", "Approval");
            DropColumn("dbo.CuttingSheetDetails", "qtyApproved");
        }
    }
}
