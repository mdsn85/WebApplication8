namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdhjf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lpoes", "StampDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lpoes", "StampDate");
        }
    }
}
