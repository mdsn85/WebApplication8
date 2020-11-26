namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        WarehouseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.WarehouseId);
            
            AddColumn("dbo.Materials", "WareHouseId", c => c.Int(nullable: true));
            CreateIndex("dbo.Materials", "WareHouseId");
            AddForeignKey("dbo.Materials", "WareHouseId", "dbo.Warehouses", "WarehouseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materials", "WareHouseId", "dbo.Warehouses");
            DropIndex("dbo.Materials", new[] { "WareHouseId" });
            DropColumn("dbo.Materials", "WareHouseId");
            DropTable("dbo.Warehouses");
        }
    }
}
