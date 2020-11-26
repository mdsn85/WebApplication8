namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsckj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "TypeOfPayment", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "PosRef", c => c.String());
            AddColumn("dbo.Payments", "Remarks", c => c.String());
            AlterColumn("dbo.Projects", "ActualDeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "ActualDeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Payments", "Remarks");
            DropColumn("dbo.Payments", "PosRef");
            DropColumn("dbo.Payments", "TypeOfPayment");
        }
    }
}
