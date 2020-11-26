namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.Customers", "Attention", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "tel", c => c.String(maxLength: 15));
            AlterColumn("dbo.Customers", "mobile", c => c.String(maxLength: 15));
            AlterColumn("dbo.Customers", "email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 150));
            CreateIndex("dbo.Customers", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "Name" });
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "email", c => c.String());
            AlterColumn("dbo.Customers", "mobile", c => c.String());
            AlterColumn("dbo.Customers", "tel", c => c.String());
            AlterColumn("dbo.Customers", "Attention", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
        }
    }
}
