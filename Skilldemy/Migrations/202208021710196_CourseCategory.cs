namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Category", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Category");
        }
    }
}
