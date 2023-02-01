namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseRatingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        UUID = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CourseRatings");
        }
    }
}
