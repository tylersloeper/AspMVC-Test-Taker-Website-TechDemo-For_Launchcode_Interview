using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Asp.net_MVC_TestpreparationAppDemo.Models
{
    public class MyQuestion
    {
        public int ID { get; set; }
        public string QuestionDescription { get; set; }
        public string MultipleChoiceCorrect { get; set; }
        public string MultipleChoiceB { get; set; }
        public string MultipleChoiceC { get; set; }
        public string MultipleChoiceD { get; set; }
        public string Answerexplanation { get; set; }

    }

    //testinbank is generated first. Then in Edit there will need to be a way to add testcontents elements to the testinbank list, and then save to database.
    public class MyAssessment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MyQuestion> AllTestQuestions = new List<MyQuestion>();

    }


    public class MyAssessmentDbContext : DbContext
    {
        public DbSet<MyAssessment> TestDataBase { get; set; }
    }

    public class MyQustionsDbContext : DbContext
    {
        public DbSet<MyQuestion> TestQuestionDataBase { get; set; }
    }


}



