namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhjdjhkjkjw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lpoes", "SubTotal", c => c.Single(nullable: false));
            AddColumn("dbo.Lpoes", "Discount", c => c.Single(nullable: false));
            AddColumn("dbo.Lpoes", "Vat", c => c.Single(nullable: false));
            AddColumn("dbo.Lpoes", "GrandTotal", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lpoes", "GrandTotal");
            DropColumn("dbo.Lpoes", "Vat");
            DropColumn("dbo.Lpoes", "Discount");
            DropColumn("dbo.Lpoes", "SubTotal");
        }
    }
}
