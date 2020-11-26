namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggslksl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectFiles", "ValidUntil", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectFiles", "ValidUntil", c => c.DateTime(nullable: false));
        }
    }
}
