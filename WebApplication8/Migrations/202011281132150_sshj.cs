namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sshj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.suppliers", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.suppliers", "Attention", c => c.String(maxLength: 150));
            AlterColumn("dbo.suppliers", "tel", c => c.String(maxLength: 20));
            AlterColumn("dbo.suppliers", "mobile", c => c.String(maxLength: 20));
            AlterColumn("dbo.suppliers", "email", c => c.String(maxLength: 100));
            AlterColumn("dbo.suppliers", "Address", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.suppliers", "Address", c => c.String());
            AlterColumn("dbo.suppliers", "email", c => c.String());
            AlterColumn("dbo.suppliers", "mobile", c => c.String());
            AlterColumn("dbo.suppliers", "tel", c => c.String());
            AlterColumn("dbo.suppliers", "Attention", c => c.String());
            AlterColumn("dbo.suppliers", "Name", c => c.String(nullable: false));
        }
    }
}
