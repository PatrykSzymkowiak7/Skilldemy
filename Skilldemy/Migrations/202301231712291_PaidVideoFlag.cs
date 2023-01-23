namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaidVideoFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "IsPaidVideo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "IsPaidVideo");
        }
    }
}
