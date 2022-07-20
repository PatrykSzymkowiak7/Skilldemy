namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "HtmlAndJs", c => c.String(unicode: false));
            AddColumn("dbo.Courses", "CreatedDate", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.Courses", "temp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "temp", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "Title", c => c.String(unicode: false));
            DropColumn("dbo.Courses", "CreatedDate");
            DropColumn("dbo.Courses", "HtmlAndJs");
        }
    }
}
