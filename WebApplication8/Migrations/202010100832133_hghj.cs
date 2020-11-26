namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hghj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PermissionId);
            
            CreateTable(
                "dbo.PermissionUsers",
                c => new
                    {
                        PermissionUserId = c.Int(nullable: false, identity: true),
                        PermissionId = c.Int(nullable: false),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.PermissionUserId)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.PermissionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionUsers", "PermissionId", "dbo.Permissions");
            DropIndex("dbo.PermissionUsers", new[] { "PermissionId" });
            DropTable("dbo.PermissionUsers");
            DropTable("dbo.Permissions");
        }
    }
}
