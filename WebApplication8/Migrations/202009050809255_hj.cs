namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "latestPrice", c => c.Single(nullable: false));
            AddColumn("dbo.Materials", "barcode", c => c.String());
            AddColumn("dbo.Materials", "code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materials", "code");
            DropColumn("dbo.Materials", "barcode");
            DropColumn("dbo.Materials", "latestPrice");
        }
    }
}
