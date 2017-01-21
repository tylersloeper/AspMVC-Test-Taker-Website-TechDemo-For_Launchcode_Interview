namespace Asp.net_MVC_TestpreparationAppDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupingId1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DualDatabaseTestSchemeQuestions", "GroupingId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DualDatabaseTestSchemeQuestions", "GroupingId");
        }
    }
}
