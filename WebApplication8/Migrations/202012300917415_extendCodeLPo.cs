namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extendCodeLPo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lpoes", "code", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lpoes", "code", c => c.String(maxLength: 15));
        }
    }
}
