namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djddfj1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "StampDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "StampDate");
        }
    }
}
