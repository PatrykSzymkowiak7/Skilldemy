namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Widocznosc_Kursu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "IsVisible");
        }
    }
}
