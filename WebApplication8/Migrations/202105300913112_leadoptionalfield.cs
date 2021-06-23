namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leadoptionalfield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leads", "ClosuerMonth", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leads", "ClosuerMonth", c => c.Int(nullable: false));
        }
    }
}
