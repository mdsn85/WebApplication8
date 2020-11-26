namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hjj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lpoes", "QuotationRef", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lpoes", "QuotationRef");
        }
    }
}
