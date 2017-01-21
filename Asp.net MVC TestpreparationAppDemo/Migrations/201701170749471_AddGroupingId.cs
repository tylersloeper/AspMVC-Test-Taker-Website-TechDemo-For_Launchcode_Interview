namespace Asp.net_MVC_TestpreparationAppDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupingId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DualDatabaseTestSchemeQuestions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionDescription = c.String(),
                        MultipleChoiceCorrect = c.String(),
                        MultipleChoiceB = c.String(),
                        MultipleChoiceC = c.String(),
                        MultipleChoiceD = c.String(),
                        Answerexplanation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DualDatabaseTestSchemeQuestions");
        }
    }
}
