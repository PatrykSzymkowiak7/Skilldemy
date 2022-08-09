namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Courses", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Category", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.Courses", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
