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
    public class MyAssessmentsController : Controller
    {
        private MyAssessmentDbContext db = new MyAssessmentDbContext();

        // GET: MyAssessments
        public ActionResult Index()
        {
            return View(db.TestDataBase.ToList());
        }

        // GET: MyAssessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAssessment myAssessment = db.TestDataBase.Find(id);
            if (myAssessment == null)
            {
                return HttpNotFound();
            }
            return View(myAssessment);
        }

        // GET: MyAssessments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyAssessments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description")] MyAssessment myAssessment)
        {
            if (ModelState.IsValid)
            {
                db.TestDataBase.Add(myAssessment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myAssessment);
        }

        // GET: MyAssessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAssessment myAssessment = db.TestDataBase.Find(id);
            if (myAssessment == null)
            {
                return HttpNotFound();
            }
            return View(myAssessment);
        }
        //mycode
        //overload that handles additional info. stil not sure how to get overload to work in asp.net
        public ActionResult EditQuestions(int id, string question, string answer, string b, string c, string d)
        {


            MyQuestion newquestiontoadd = new MyQuestion();
            newquestiontoadd.QuestionDescription = question;
            newquestiontoadd.MultipleChoiceCorrect = answer;
            newquestiontoadd.MultipleChoiceB = b;
            newquestiontoadd.MultipleChoiceC = c;
            newquestiontoadd.MultipleChoiceD = d;
            newquestiontoadd.Answerexplanation = "Currently not supported.";

            MyAssessment myTests = new MyAssessment();
            myTests = db.TestDataBase.Find(id);
            if (myTests == null)
            {
                return HttpNotFound();
            }

            //assingment
            myTests.AllTestQuestions.Add(newquestiontoadd);
            myTests.AllTestQuestions.Add(newquestiontoadd);
            /*if I can then save to database right here. it will work. I have the item and the list is added to 
             * the temp item that is identical to what is in the database. but the database hasnt been changed yet
             * and in edit im still pulling the item directly from the database. so in the end I have two items.
             * and i need to either make one equal to the other, or i need to save the new one to the database right here.
             * saving to the database right here would be best. If I can figure out how.*/
            db.TestDataBase.Attach(myTests);
            db.Entry(myTests).State = EntityState.Modified;
            db.SaveChanges();


            return View(db.TestDataBase.Find(id));
            //return View(myTests);
            //return View(newquestiontoadd);
        }

        public ActionResult TestObject()
        {
            Random random = new Random();
            MyAssessment Assessment90 = new MyAssessment();
            MyQuestion Question90 = new MyQuestion();

            Question90.ID = 2; //random.Next(50, 5000);
            Question90.QuestionDescription = "Hello?";
            Question90.MultipleChoiceCorrect = "a";
            Question90.MultipleChoiceB = "b";
            Question90.MultipleChoiceC = "c";
            Question90.MultipleChoiceD = "d";
            Question90.QuestionDescription = "Found?";


            Assessment90.ID = random.Next(50, 5000);
            Assessment90.Name = "Test90";
            Assessment90.Description = "Description90";

            for(int i = 1; i<50;i++)
            {
            Assessment90.AllTestQuestions.Add(Question90);
            }

            db.TestDataBase.Add(Assessment90);
            db.SaveChanges();

            return RedirectToAction("Index");
        }





        // POST: MyAssessments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

            //this is the problem. it is not saving the modified thing to database. or rather it is not saving the list.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,AllTestQuestions")] MyAssessment myAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myAssessment);
        }

        //myversion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestions([Bind(Include = "ID,Name,Description,AllTestQuestions")] MyAssessment myAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myAssessment);
        }

        // GET: MyAssessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAssessment myAssessment = db.TestDataBase.Find(id);
            if (myAssessment == null)
            {
                return HttpNotFound();
            }
            return View(myAssessment);
        }

        // POST: MyAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyAssessment myAssessment = db.TestDataBase.Find(id);
            db.TestDataBase.Remove(myAssessment);
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
    }
}
