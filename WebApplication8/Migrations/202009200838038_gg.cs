namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "SalePrice", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "SalePrice", c => c.Single(nullable: false));
        }
    }
}
