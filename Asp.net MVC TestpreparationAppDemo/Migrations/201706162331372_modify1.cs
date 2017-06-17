namespace Asp.net_MVC_TestpreparationAppDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DualDatabaseTestSchemeTests", "Owner", c => c.String());
            AddColumn("dbo.DualDatabaseTestSchemeTests", "Upvotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DualDatabaseTestSchemeTests", "Upvotes");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "Owner");
        }
    }
}
