namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotherUserModelChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Town", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Town");
        }
    }
}
