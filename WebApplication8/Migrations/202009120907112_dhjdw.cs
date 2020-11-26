namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhjdw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lpoes", "Sequense", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lpoes", "Sequense");
        }
    }
}
