namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fhfw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "Dimension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materials", "Dimension");
        }
    }
}
