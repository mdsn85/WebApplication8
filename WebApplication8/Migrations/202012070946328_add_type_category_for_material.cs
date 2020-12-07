namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_type_category_for_material : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialCategories",
                c => new
                    {
                        MaterialCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MaterialCategoryId);
            
            CreateTable(
                "dbo.MaterialTypes",
                c => new
                    {
                        MaterialTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MaterialTypeId);
            
            AddColumn("dbo.Materials", "MaterialCategoryId", c => c.Int());
            AddColumn("dbo.Materials", "MaterialTypeId", c => c.Int());
            AlterColumn("dbo.Materials", "Name", c => c.String(maxLength: 250));
            CreateIndex("dbo.Materials", "MaterialCategoryId");
            CreateIndex("dbo.Materials", "MaterialTypeId");
            AddForeignKey("dbo.Materials", "MaterialCategoryId", "dbo.MaterialCategories", "MaterialCategoryId");
            AddForeignKey("dbo.Materials", "MaterialTypeId", "dbo.MaterialTypes", "MaterialTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materials", "MaterialTypeId", "dbo.MaterialTypes");
            DropForeignKey("dbo.Materials", "MaterialCategoryId", "dbo.MaterialCategories");
            DropIndex("dbo.Materials", new[] { "MaterialTypeId" });
            DropIndex("dbo.Materials", new[] { "MaterialCategoryId" });
            AlterColumn("dbo.Materials", "Name", c => c.String());
            DropColumn("dbo.Materials", "MaterialTypeId");
            DropColumn("dbo.Materials", "MaterialCategoryId");
            DropTable("dbo.MaterialTypes");
            DropTable("dbo.MaterialCategories");
        }
    }
}
