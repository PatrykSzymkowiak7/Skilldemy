namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelVideo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "IsPreviewVideo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "IsPreviewVideo");
        }
    }
}
