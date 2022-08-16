namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SectionsV2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Videos", "VideoFile", c => c.Binary());
            AlterColumn("dbo.Videos", "SectionTitle", c => c.String(unicode: false));
            AlterColumn("dbo.Videos", "SectionDescription", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "SectionDescription", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Videos", "SectionTitle", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Videos", "VideoFile", c => c.Binary(nullable: false));
        }
    }
}
