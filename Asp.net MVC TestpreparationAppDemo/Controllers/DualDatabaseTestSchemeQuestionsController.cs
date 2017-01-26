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
        [Authorize(Roles = "canEdit")]
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
        [Authorize(Roles = "canEdit")]
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
        [Authorize(Roles = "canEdit")]
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
        [Authorize(Roles = "canEdit")]
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

        [Authorize(Roles = "canEdit")]
        public ActionResult AddNewQuestionAction(string id, string question, string answer, string optionb, string optionc, string optiond)
        {
            DualDatabaseTestSchemeQuestion newQuestion = new DualDatabaseTestSchemeQuestion();
            //int ID = id;
            int newID = 99;
            Int32.TryParse(id, out newID);

            newQuestion.GroupingId = newID;
            newQuestion.QuestionDescription = question; //+ " :  ID = " + newID;
            newQuestion.MultipleChoiceCorrect = answer;
            newQuestion.MultipleChoiceB = optionb;
            newQuestion.MultipleChoiceC = optionc;
            newQuestion.MultipleChoiceD = optiond;

            db.DualDatabaseTestSchemeQuestionDataBase.Add(newQuestion);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        /*
        //this search/filter method works for by string.
        public ActionResult ViewQuestions(string searchString)
        {
            var questions = from m in db.DualDatabaseTestSchemeQuestionDataBase
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(s => s.QuestionDescription.Contains(searchString));
            }

            return View(questions);
        }
        */

        /*    //and this works for filtering by groupingid
        public ActionResult ViewQuestions(int? searchid)
        {

            var questions = from m in db.DualDatabaseTestSchemeQuestionDataBase
                            select m;
            if (searchid != null)
            {
                questions = questions.Where(s => s.GroupingId == searchid);
            }
            return View(questions);

        } */

        
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

            /* Globals in this context have been replaced by tempdata.
            //Globals.GlobalQuestionList = new List<DualDatabaseTestSchemeQuestion>();
            //Globals.GlobalQuestionList = tempQuestionlist; //commented out to remove globals.
            //attempting to remove globals using tempdata.
            */

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


            return RedirectToAction("CustomTest", "Quizbank");

        }

        [Authorize(Roles = "canEdit")]
        public ActionResult testnewcode()
        {
            //return RedirectToAction("Index", "Quizbank", new { id = "89" });
            return RedirectToAction("Index", "Quizbank");
        }


    }
}
