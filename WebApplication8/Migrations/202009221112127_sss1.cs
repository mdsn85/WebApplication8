namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sss1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeUsers",
                c => new
                    {
                        EmployeeUserId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeUsers");
        }
    }
}
