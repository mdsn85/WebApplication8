namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hl1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PermissionUsers", "PermissionId", "dbo.Permissions");
            DropIndex("dbo.PermissionUsers", new[] { "PermissionId" });
            //DropTable("dbo.Permissions");
            //DropTable("dbo.PermissionUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PermissionUsers",
                c => new
                    {
                        PermissionUserId = c.Int(nullable: false, identity: true),
                        PermissionId = c.Int(nullable: false),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.PermissionUserId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PermissionId);
            
            CreateIndex("dbo.PermissionUsers", "PermissionId");
            AddForeignKey("dbo.PermissionUsers", "PermissionId", "dbo.Permissions", "PermissionId", cascadeDelete: true);
        }
    }
}
