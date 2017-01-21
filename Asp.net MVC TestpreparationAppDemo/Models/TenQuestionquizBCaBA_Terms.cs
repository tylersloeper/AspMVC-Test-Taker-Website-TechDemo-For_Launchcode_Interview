using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp.net_MVC_TestpreparationAppDemo.Models
{

    public class question
    {
        public string Question;
        public string Answer;

        //in controller logic match answer to the option selected and check if the answer is correct by matching ==. for now options will be filled with gibberish
        public string OptionA;
        public string OptionB;
        public string OptionC;
        public string OptionD;

    }

    public static class TenQuestionquizBCaBA_Terms
    {
       

        //usage TenQuestionquizBCaBA_Terms.ChooseRandomQuestion(a,b);
        //return a randomly selected question from the question bank [100] intended for quizes of length 10 questions.
        public static void ChooseRandomQuestion(string Question, string Answer)
        {
            Question = "question";
            Answer = "answer";
            
        }
        //overload2
        //usage TenQuestionquizBCaBA_Terms.ChooseRandomQuestion(class);
        public static void ChooseRandomQuestion(question questionClass)
        {
            questionClass.Question = "question";
            questionClass.Answer = "answer";

        }
    }
}