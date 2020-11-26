namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Materials", "OpeningQty", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materials", "OpeningQty");
            DropColumn("dbo.Materials", "CreatedDate");
        }
    }
}
