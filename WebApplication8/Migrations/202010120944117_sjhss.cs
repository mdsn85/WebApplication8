namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sjhss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CuttingSheetDetails", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CuttingSheetDetails", "Remark", c => c.Single(nullable: false));
        }
    }
}
