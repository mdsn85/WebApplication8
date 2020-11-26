namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djdjpsd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lpoes", "UserCreate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lpoes", "UserCreate");
        }
    }
}
