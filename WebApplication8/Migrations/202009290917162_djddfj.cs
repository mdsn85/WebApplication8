namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djddfj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Deduction", c => c.Single());
            AddColumn("dbo.Projects", "DeductionReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "DeductionReason");
            DropColumn("dbo.Projects", "Deduction");
        }
    }
}
