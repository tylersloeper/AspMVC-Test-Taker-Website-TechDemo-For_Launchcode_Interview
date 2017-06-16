using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp.net_MVC_TestpreparationAppDemo.Models;

namespace Asp.net_MVC_TestpreparationAppDemo.Controllers
{
    public class DualDatabaseTestSchemeQuestionsController : Controller
    {
        private DualDatabaseTestSchemeQuestionsDbContext db = new DualDatabaseTestSchemeQuestionsDbContext();

        // GET: DualDatabaseTestSchemeQuestions

        public ActionResult Index()
        {
            return View(db.DualDatabaseTestSchemeQuestionDataBase.ToList());
        }

        // GET: DualDatabaseTestSchemeQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DualDatabaseTestSchemeQuestion dualDatabaseTestSchemeQuestion = db.DualDatabaseTestSchemeQuestionDataBase.Find(id);
            if (dualDatabaseTestSchemeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(dualDatabaseTestSchemeQuestion);
        }

        // GET: DualDatabaseTestSchemeQuestions/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: DualDatabaseTestSchemeQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "ID,QuestionDescription,MultipleChoiceCorrect,MultipleChoiceB,MultipleChoiceC,MultipleChoiceD,Answerexplanation")] DualDatabaseTestSchemeQuestion dualDatabaseTestSchemeQuestion)
        {
            if (ModelState.IsValid)
            {
                db.DualDatabaseTestSchemeQuestionDataBase.Add(dualDatabaseTestSchemeQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dualDatabaseTestSchemeQuestion);
        }

        // GET: DualDatabaseTestSchemeQuestions/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DualDatabaseTestSchemeQuestion dualDatabaseTestSchemeQuestion = db.DualDatabaseTestSchemeQuestionDataBase.Find(id);
            if (dualDatabaseTestSchemeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(dualDatabaseTestSchemeQuestion);
        }

        // POST: DualDatabaseTestSchemeQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "ID,QuestionDescription,MultipleChoiceCorrect,MultipleChoiceB,MultipleChoiceC,MultipleChoiceD,Answerexplanation,GroupingId")] DualDatabaseTestSchemeQuestion dualDatabaseTestSchemeQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dualDatabaseTestSchemeQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dualDatabaseTestSchemeQuestion);
        }

        // GET: DualDatabaseTestSchemeQuestions/Delete/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DualDatabaseTestSchemeQuestion dualDatabaseTestSchemeQuestion = db.DualDatabaseTestSchemeQuestionDataBase.Find(id);
            if (dualDatabaseTestSchemeQuestion == null)
            {
                return HttpNotFound();
            }
            return View(dualDatabaseTestSchemeQuestion);
        }

        // POST: DualDatabaseTestSchemeQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            DualDatabaseTestSchemeQuestion dualDatabaseTestSchemeQuestion = db.DualDatabaseTestSchemeQuestionDataBase.Find(id);
            db.DualDatabaseTestSchemeQuestionDataBase.Remove(dualDatabaseTestSchemeQuestion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult AddNewQuestionAction(string id, string question, string answer, string optionb, string optionc, string optiond)
        {
            DualDatabaseTestSchemeQuestion newQuestion = new DualDatabaseTestSchemeQuestion();
            
            int newID = 0;
            Int32.TryParse(id, out newID);

            newQuestion.GroupingId = newID;
            newQuestion.QuestionDescription = question; //+ " :  ID = " + newID;
            newQuestion.MultipleChoiceCorrect = answer;
            newQuestion.MultipleChoiceB = optionb;
            newQuestion.MultipleChoiceC = optionc;
            newQuestion.MultipleChoiceD = optiond;

            db.DualDatabaseTestSchemeQuestionDataBase.Add(newQuestion);
            db.SaveChanges();

            //pass id to add another question view
            TempData["id"] = id;

            return RedirectToAction("AddAnotherQuestion");
        }

        public ActionResult AddAnotherQuestion()
        {
    
            ViewBag.id = (string)TempData["id"];

            return View();
        }


        
          //this is my final version to try and put this thing in a list.

        public ActionResult ViewQuestions(int? searchid)
        {

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

            return View(questions);

        }

        public ActionResult ViewGlobals()
        {

            ViewBag.count = Globals.GlobalQuestionList.Count();

            return View(Globals.GlobalQuestionList);
        }

        //argument is not being passed to this correctly.
        public ActionResult CustomTestPreFormatting(int? searchid)
        {
            /*steps
             * 1. get test id
             * 2. use test id to get all related questions from the database
             * 3.save related questions to globals list //will be using tempdata now.
             * 4.redirect
             * */

            var questions = from m in db.DualDatabaseTestSchemeQuestionDataBase
                            select m;
            if (searchid != null)
            {
                questions = questions.Where(s => s.GroupingId == searchid);
            }

            var tempQuestionlist = new List<DualDatabaseTestSchemeQuestion>();
            tempQuestionlist = questions.ToList<DualDatabaseTestSchemeQuestion>();

            TempData["tempQuestionlist"] = tempQuestionlist;

            //create a tempdata list to show answers in the final results page
            var reviewlist = new List<DualDatabaseTestSchemeQuestion>();
            TempData["reviewlist"] = reviewlist;
            //create a tempdata list to track correct or incorrect answers.
            var CorrectorIncorrect = new List<string>();
            TempData["CorrectorIncorrect"] = CorrectorIncorrect;

            //debugging on this methods view
            ViewBag.globals = Globals.GlobalQuestionList;
            ViewBag.count = Globals.GlobalQuestionList.Count();

            //attempt to swith question authetication over to temp data
            double questionscorrect = 0;
            double questionswrong = 0;
            int questioncount = 1;
            TempData["questionscorrect"] = questionscorrect;
            TempData["questionswrong"] = questionswrong;
            TempData["questioncount"] = questioncount;


            return RedirectToAction("CustomTest", "Quizbank");

        }


        public ActionResult testnewcode()
        {
            return RedirectToAction("Index", "Quizbank");
        }


        public ActionResult CountQuestionsInTest(int? id)
        {
            var questions = from m in db.DualDatabaseTestSchemeQuestionDataBase
                            select m;
            if (id != null)
            {
                questions = questions.Where(s => s.GroupingId == id);
            }

            int countquestions = questions.Count<DualDatabaseTestSchemeQuestion>();
            TempData["countquestions"] = countquestions;

            return RedirectToAction("Details/" + id, "DualDatabaseTestSchemeTests");
            //return View();
        }

        private static Random randomroll = new Random();
        private static Random random = new Random();
        //prints 10 questions with randmozed elements to a page.
        public ActionResult TestPDF(int? id, string title)
        {
            var questions = from m in db.DualDatabaseTestSchemeQuestionDataBase
                            select m;
            if (id != null)
            {
                questions = questions.Where(s => s.GroupingId == id);
            }

            //list to contain all filtered questions.
            var tempQuestionlist = new List<DualDatabaseTestSchemeQuestion>();
            tempQuestionlist = questions.ToList<DualDatabaseTestSchemeQuestion>();
            //list containing 10 fully scrambled questions.
            var UpdatedtempQuestionlist = new List<DualDatabaseTestSchemeQuestion>();
            int count = tempQuestionlist.Count();
            //scramble questions and add them to updated list 10 times (10 questions total).
            for(int m = 0; m<10;m++)
            { 
            //create new instance of objects
            DualDatabaseTestSchemeQuestion randomquestion = new DualDatabaseTestSchemeQuestion();
            DualDatabaseTestSchemeQuestion tempquestion = new DualDatabaseTestSchemeQuestion();

            //choose a random question from questionlist
            int randomizeanswers;
            int choose = random.Next(0, count);
            randomquestion = tempQuestionlist[choose];


            List<string> scrambledanswers = new List<string>();
            scrambledanswers.Add(randomquestion.MultipleChoiceCorrect);
            scrambledanswers.Add(randomquestion.MultipleChoiceB);
            scrambledanswers.Add(randomquestion.MultipleChoiceC);
            scrambledanswers.Add(randomquestion.MultipleChoiceD);

            //assignanswervalues scrambled to tempquestion from random question.
            tempquestion.QuestionDescription = randomquestion.QuestionDescription;
            //for pdf version there is no answer authentication. Position does not correspond to
            //correctness
            //positionA
            randomizeanswers = randomroll.Next(0, 3);
            tempquestion.MultipleChoiceCorrect = scrambledanswers[randomizeanswers];
            scrambledanswers.RemoveAt(randomizeanswers);
            //positionB
            randomizeanswers = randomroll.Next(0, 2);
            tempquestion.MultipleChoiceB = scrambledanswers[randomizeanswers];
            scrambledanswers.RemoveAt(randomizeanswers);
            //positionC
            randomizeanswers = randomroll.Next(0, 1);
            tempquestion.MultipleChoiceC = scrambledanswers[randomizeanswers];
            scrambledanswers.RemoveAt(randomizeanswers);
            //positionD
            tempquestion.MultipleChoiceD = scrambledanswers[0];

            //add modified question to list
            UpdatedtempQuestionlist.Add(tempquestion);
            //repeat 10 times
            }

            TempData["UpdatedtempQuestionlist"] = UpdatedtempQuestionlist;
            TempData["title"] = title;
            //return modifed list
            return RedirectToAction("ShowPDF", "Quizbank");
        }


    }
}
