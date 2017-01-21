using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.net_MVC_TestpreparationAppDemo.Models;

namespace Asp.net_MVC_TestpreparationAppDemo.Controllers
{
    public class QuizbankController : Controller
    {
        // GET: Quizbank
        [AllowAnonymous]
        public ActionResult Index()
        {
            Globals.count = 1;
            Globals.answerscorrect = 0;
            Globals.answerswrong = 0;

            return View();
        }

        [AllowAnonymous]
        public ActionResult TestQuiz()
        {

            string mockquestion;
            string mockansweroptionA;
            string mockansweroptionB;
            string mockansweroptionC;
            string mockansweroptionD;

            string temp;

            mockquestion = Randomfilleranswers.Returnfilleranswer();
            mockansweroptionA = Randomfilleranswers.Returnfilleranswer();
            mockansweroptionB = Randomfilleranswers.Returnfilleranswer();
            mockansweroptionC = Randomfilleranswers.Returnfilleranswer();
            mockansweroptionD = Randomfilleranswers.Returnfilleranswer();

            ViewBag.mockquestion = mockquestion;
            ViewBag.mockansweroptionA = mockansweroptionA;
            ViewBag.mockansweroptionB = mockansweroptionB;
            ViewBag.mockansweroptionC = mockansweroptionC;
            ViewBag.mockansweroptionD = mockansweroptionD;

            
            temp    = Randomfilleranswers.ReturnCorrectOrIncorrect();
            ViewBag.choiceA = temp;
            temp = Randomfilleranswers.ReturnCorrectOrIncorrect();
            ViewBag.choiceB = temp;
            temp = Randomfilleranswers.ReturnCorrectOrIncorrect();
            ViewBag.choiceC = temp;
            temp = Randomfilleranswers.ReturnCorrectOrIncorrect();
            ViewBag.choiceD = temp;


            ViewBag.amountcorrect = Globals.answerscorrect;
            ViewBag.amountwrong = Globals.answerswrong;
            ViewBag.count = Globals.count;



            return View();
        }

        [AllowAnonymous]
        public ActionResult checkTestQuiz(string answer)
        {

            Globals.count = Globals.count + 1;

            if (Globals.count > 10)
            {
                Globals.count = 1;
                Globals.answerscorrect = 0;
                Globals.answerswrong = 0;
                return RedirectToAction("TestQuizFinalResults");
            }
            if (answer == "correct")
            {
                Globals.answerscorrect = Globals.answerscorrect + 1d;
                Globals.delayedanswerscorrect = Globals.answerscorrect;

                return RedirectToAction("TestQuiz");
            }
            if (answer == "incorrect")
            {
                Globals.answerswrong = Globals.answerswrong + 1d;
                Globals.delayedanswerswrong = Globals.answerswrong;
                return RedirectToAction("TestQuiz");
            }

            return View();
        }

        public ActionResult TestQuizFinalResults()
        {
            ViewBag.amountcorrect = Globals.delayedanswerscorrect;
            ViewBag.amountwrong = Globals.delayedanswerswrong;
            ViewBag.count = Globals.count;

            return View();
        }
/*
        public ActionResult CustomTestPreFormatting(int? searchid)
        {
            /*steps
             * 1. get test id
             * 2. use test id to get all related questions from the database
             * 3.save related questions to globals list
             * 4.redirect
             * 

            var questions = from m in db.DualDatabaseTestSchemeQuestionDataBase
                            select m;
            if (searchid != null)
            {
                questions = questions.Where(s => s.GroupingId == searchid);
            }

            //new stuff here

            var tempQuestionlist = new List<DualDatabaseTestSchemeQuestion>();
            tempQuestionlist = questions.ToList<DualDatabaseTestSchemeQuestion>();
            Globals.GlobalQuestionList = tempQuestionlist;

            ViewBag.globals = Globals.GlobalQuestionList;


            return RedirectToAction("CustomTest");
        }*/

        public ActionResult CustomTest()
        {
            /*steps
             * does not require id since it using data from globals.
             * 1. input values from one randomly selected item from globals into viewbags or else return the model. 
             * ill try model.
             * 2. redirect to self and update globals
             * */

            int count = Globals.GlobalQuestionList.Count();
            Random random = new Random();
            int choose = random.Next(0, count);

            DualDatabaseTestSchemeQuestion randomquestion = new DualDatabaseTestSchemeQuestion();
            randomquestion = Globals.GlobalQuestionList[choose];
            //randomquestion = Globals.GlobalQuestionList.ElementAt(choose);

            ViewBag.amountcorrect = Globals.answerscorrect;
            ViewBag.amountwrong = Globals.answerswrong;
            ViewBag.count = Globals.count;

            return View(randomquestion);
        }

        public ActionResult CustomTestCheckAnswers(string answer)
        {

            Globals.count = Globals.count + 1;

            if (Globals.count > 10)
            {
                Globals.count = 1;
                Globals.answerscorrect = 0;
                Globals.answerswrong = 0;
                return RedirectToAction("TestQuizFinalResults");
            }
            if (answer == "correct")
            {
                Globals.answerscorrect = Globals.answerscorrect + 1d;
                Globals.delayedanswerscorrect = Globals.answerscorrect;

                return RedirectToAction("CustomTest");
            }
            if (answer == "incorrect")
            {
                Globals.answerswrong = Globals.answerswrong + 1d;
                Globals.delayedanswerswrong = Globals.answerswrong;
                return RedirectToAction("CustomTest");
            }

            return View();
        }


    }
}