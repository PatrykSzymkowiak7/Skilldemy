namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "SectionTitle", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.Videos", "SectionDescription", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Videos", "VideoFile", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "VideoFile", c => c.Binary());
            DropColumn("dbo.Videos", "SectionDescription");
            DropColumn("dbo.Videos", "SectionTitle");
        }
    }
}
