namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhjdwww : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lpoes", "code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lpoes", "code", c => c.Int(nullable: false));
        }
    }
}
