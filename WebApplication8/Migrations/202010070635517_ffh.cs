namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ffh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "LastUpdateDate", c => c.DateTime());
            AddColumn("dbo.Projects", "UserLastUpdate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "UserLastUpdate");
            DropColumn("dbo.Projects", "LastUpdateDate");
        }
    }
}
