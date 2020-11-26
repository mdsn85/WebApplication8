namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hdhd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "DateOfCredit", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "DateOfCredit", c => c.DateTime(nullable: false));
        }
    }
}
