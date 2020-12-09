namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class warantydateOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Warranty", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Warranty", c => c.DateTime(nullable: false));
        }
    }
}
