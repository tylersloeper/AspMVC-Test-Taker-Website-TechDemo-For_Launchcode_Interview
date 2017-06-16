namespace Asp.net_MVC_TestpreparationAppDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DualDatabaseTestSchemeTests", "Status", c => c.String());
            AddColumn("dbo.DualDatabaseTestSchemeTests", "Genre", c => c.String());
            AddColumn("dbo.DualDatabaseTestSchemeTests", "DateModified", c => c.String());
            AddColumn("dbo.DualDatabaseTestSchemeTests", "GroupingId", c => c.Int(nullable: false));
            AddColumn("dbo.DualDatabaseTestSchemeTests", "TimesTaken", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DualDatabaseTestSchemeTests", "TimesTaken");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "GroupingId");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "DateModified");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "Genre");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "Status");
        }
    }
}
