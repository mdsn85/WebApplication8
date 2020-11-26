namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "AvalableQty", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materials", "AvalableQty");
        }
    }
}
