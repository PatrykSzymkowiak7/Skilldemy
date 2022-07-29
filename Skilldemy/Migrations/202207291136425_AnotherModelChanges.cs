namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherModelChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "OwnerUserName", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "OwnerUserName");
        }
    }
}
