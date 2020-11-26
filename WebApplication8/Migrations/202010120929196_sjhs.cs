namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sjhs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuttingSheetDetails", "Remark", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CuttingSheetDetails", "Remark");
        }
    }
}
