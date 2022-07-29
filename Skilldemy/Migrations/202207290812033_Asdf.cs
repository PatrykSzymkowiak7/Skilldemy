namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Asdf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Courses", "OwnerId", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "OwnerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "Description", c => c.String(unicode: false));
        }
    }
}
