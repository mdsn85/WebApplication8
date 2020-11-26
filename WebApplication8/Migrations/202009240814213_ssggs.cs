namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssggs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "designation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "designation");
        }
    }
}
