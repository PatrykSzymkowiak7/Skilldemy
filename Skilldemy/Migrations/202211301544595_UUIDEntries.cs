namespace Skilldemy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UUIDEntries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UUID = c.String(unicode: false),
                        CourseId = c.Int(nullable: false),
                        EntryTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UUIDConnections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UUID = c.String(unicode: false),
                        CourseId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UUIDConnections");
            DropTable("dbo.CourseEntries");
        }
    }
}
