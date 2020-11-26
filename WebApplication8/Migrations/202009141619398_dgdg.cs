namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dgdg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.suppliers", "CreditTerm", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.suppliers", "CreditTerm");
        }
    }
}
