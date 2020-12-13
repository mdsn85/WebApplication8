namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationCatagoryModify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationCategories", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotificationCategories", "Name");
        }
    }
}
