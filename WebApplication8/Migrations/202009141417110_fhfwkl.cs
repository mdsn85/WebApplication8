namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fhfwkl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "Resevedqty", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materials", "Resevedqty");
        }
    }
}
