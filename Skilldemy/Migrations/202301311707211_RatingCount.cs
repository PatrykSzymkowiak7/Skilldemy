namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "RatingCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "RatingCount");
        }
    }
}
