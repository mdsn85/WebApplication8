namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdaf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Code");
        }
    }
}
