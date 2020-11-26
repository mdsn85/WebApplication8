namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fhfwklkw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuttingSheetDetails", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CuttingSheetDetails", "status");
        }
    }
}
