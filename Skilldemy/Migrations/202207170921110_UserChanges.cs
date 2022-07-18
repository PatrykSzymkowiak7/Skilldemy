namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BankAccountNumber", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "Street", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "HouseNumber", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "FlatNumber", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "PostalCode", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PostalCode");
            DropColumn("dbo.AspNetUsers", "FlatNumber");
            DropColumn("dbo.AspNetUsers", "HouseNumber");
            DropColumn("dbo.AspNetUsers", "Street");
            DropColumn("dbo.AspNetUsers", "BankAccountNumber");
        }
    }
}
