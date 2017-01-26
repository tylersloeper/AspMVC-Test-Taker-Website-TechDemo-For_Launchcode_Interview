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

            //set up questions so they can be reviewed as correct or incorrect.
            if(TempData["CorrectorIncorrect"] != null)
            {
                var CorrectorIncorrect = TempData["CorrectorIncorrect"] as List<string>;
                ViewData["CorrectorIncorrect"] = CorrectorIncorrect;
            }


            //list questions taken in the test
            if (TempData["reviewlist"] != null)
            {
            var reviewlist = TempData["reviewlist"] as List<DualDatabaseTestSchemeQuestion>;
            return View(reviewlist);
            }


            return View();
        }


        private static Random randomroll = new Random();
        private static Random random = new Random();

        public ActionResult CustomTest()
        {
            /*steps
             * does not require id since it using data from globals.
             * 1. input values from one randomly selected item from globals (or tempdata) into viewbags or else return the model. 
             * ill try model.
             * 2. redirect to self and update globals (or tempdata).
             * */

            //attempting to remove globals using tempdata, because globals not compatible with multiple users.
            var tempQuestionlist = new List<DualDatabaseTestSchemeQuestion>();
            tempQuestionlist = TempData["tempQuestionlist"] as List<DualDatabaseTestSchemeQuestion>;
            //must save data in tempdata again so that it can be used in refresh. leaves on artifact at the end of each test.
            TempData["tempQuestionlist"] = tempQuestionlist;
            int count = tempQuestionlist.Count();

            /* Old Code using Globals. Kept in cases it is need for a rollback.
            //int count = Globals.GlobalQuestionList.Count();  //commented out for temp data.
            //DualDatabaseTestSchemeQuestion randomquestion = new DualDatabaseTestSchemeQuestion();
            //randomquestion = Globals.GlobalQuestionList[choose];
            */

            //choose a random question from questionlist
            int choose = random.Next(0, count);
            DualDatabaseTestSchemeQuestion randomquestion = new DualDatabaseTestSchemeQuestion();
            randomquestion = tempQuestionlist[choose];

            //update globals for the test subwindow
            ViewBag.amountcorrect = Globals.answerscorrect;
            ViewBag.amountwrong = Globals.answerswrong;
            ViewBag.count = Globals.count;

            //scramble arrangement of answers for chosen question but keep track of which is correct.
            ViewBag.correctincorrectstatusa = "incorrect";
            ViewBag.correctincorrectstatusb = "incorrect";
            ViewBag.correctincorrectstatusc = "incorrect";
            ViewBag.correctincorrectstatusd = "incorrect";

            List<string> scrambledanswers = new List<string>();
            scrambledanswers.Add(randomquestion.MultipleChoiceCorrect);
            scrambledanswers.Add(randomquestion.MultipleChoiceB);
            scrambledanswers.Add(randomquestion.MultipleChoiceC);
            scrambledanswers.Add(randomquestion.MultipleChoiceD);

            //assignanswervalues to random position

            int randomizeanswers;

            //positionA
            randomizeanswers = randomroll.Next(0, 3);
            ViewBag.A = scrambledanswers[randomizeanswers];
            if(scrambledanswers[randomizeanswers] == randomquestion.MultipleChoiceCorrect)
            {
                ViewBag.correctincorrectstatusa = "correct";
            }
            scrambledanswers.RemoveAt(randomizeanswers);

            //positionB
            randomizeanswers = randomroll.Next(0, 2);
            ViewBag.B = scrambledanswers[randomizeanswers];
            if (scrambledanswers[randomizeanswers] == randomquestion.MultipleChoiceCorrect)
            {
                ViewBag.correctincorrectstatusb = "correct";
            }
            scrambledanswers.RemoveAt(randomizeanswers);

            //positionC
            randomizeanswers = randomroll.Next(0, 1);
            ViewBag.C = scrambledanswers[randomizeanswers];
            if (scrambledanswers[randomizeanswers] == randomquestion.MultipleChoiceCorrect)
            {
                ViewBag.correctincorrectstatusc = "correct";
            }
            scrambledanswers.RemoveAt(randomizeanswers);

            //positionD
            ViewBag.D = scrambledanswers[0];
            if (scrambledanswers[0] == randomquestion.MultipleChoiceCorrect)
            {
                ViewBag.correctincorrectstatusd = "correct";
            }

            //assign chosen question to review list and push it forward
            var reviewlist = TempData["reviewlist"] as List<DualDatabaseTestSchemeQuestion>;
            reviewlist.Add(randomquestion);
            TempData["reviewlist"] = reviewlist;


            return View(randomquestion);
        }

        public ActionResult CustomTestCheckAnswers(string answer)
        {

            

            if(answer == "on" || answer == null)
            {
                return RedirectToAction("errorpage");
            }

            //track questions chosen so there can be a review of its status as correct or incorrect.
            //establish correct or incorrect in tempdata so it can be shown on the finalresults view
            var CorrectorIncorrect = TempData["CorrectorIncorrect"] as List<string>;

            //for correct
            if (answer == "correct")
            {
                Globals.answerscorrect = Globals.answerscorrect + 1d;
                Globals.delayedanswerscorrect = Globals.answerscorrect;
                Globals.count = Globals.count + 1;

                CorrectorIncorrect.Add("Correct");
                TempData["CorrectorIncorrect"] = CorrectorIncorrect;

                if (Globals.count > 10)
                {
                    Globals.count = 1;
                    Globals.answerscorrect = 0;
                    Globals.answerswrong = 0;
                    return RedirectToAction("TestQuizFinalResults");
                }

                return RedirectToAction("CustomTest");
            }
            //for incorrect
            if (answer == "incorrect")
            {
                Globals.answerswrong = Globals.answerswrong + 1d;
                Globals.delayedanswerswrong = Globals.answerswrong;
                Globals.count = Globals.count + 1;

                CorrectorIncorrect.Add("Incorrect");
                TempData["CorrectorIncorrect"] = CorrectorIncorrect;

                if (Globals.count > 10)
                {
                    Globals.count = 1;
                    Globals.answerscorrect = 0;
                    Globals.answerswrong = 0;
                    return RedirectToAction("TestQuizFinalResults");
                }

                return RedirectToAction("CustomTest");
            }

            return View();
        }

        public ActionResult errorpage()
        {
            return View();
        }


    }
}