namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Ceny_Z_Double_Na_Decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Price", c => c.Double(nullable: false));
        }
    }
}
