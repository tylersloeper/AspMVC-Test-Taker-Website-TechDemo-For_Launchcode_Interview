namespace Asp.net_MVC_TestpreparationAppDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class custom3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DualDatabaseTestSchemeTests", "IsAdvanced", c => c.Boolean(nullable: false));
            AddColumn("dbo.DualDatabaseTestSchemeTests", "TestFinishedDefaultMessage", c => c.String());
            AddColumn("dbo.DualDatabaseTestSchemeTests", "MessagesArrayAsSingleString", c => c.String());
            AddColumn("dbo.DualDatabaseTestSchemeTests", "AmountCorrectArrayAsSingleString", c => c.String());
            AddColumn("dbo.DualDatabaseTestSchemeTests", "AdditionalConditionsArrayAsSingleString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DualDatabaseTestSchemeTests", "AdditionalConditionsArrayAsSingleString");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "AmountCorrectArrayAsSingleString");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "MessagesArrayAsSingleString");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "TestFinishedDefaultMessage");
            DropColumn("dbo.DualDatabaseTestSchemeTests", "IsAdvanced");
        }
    }
}
