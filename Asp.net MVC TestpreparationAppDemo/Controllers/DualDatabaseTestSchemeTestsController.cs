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
    public class DualDatabaseTestSchemeTestsController : Controller
    {
        private DualDatabaseTestSchemeTestsDbContext db = new DualDatabaseTestSchemeTestsDbContext();

        // GET: DualDatabaseTestSchemeTests

        public ActionResult Index()
        {
            return View(db.DualDatabaseTestSchemeTestDataBase.ToList());
        }

        // GET: DualDatabaseTestSchemeTests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DualDatabaseTestSchemeTest dualDatabaseTestSchemeTest = db.DualDatabaseTestSchemeTestDataBase.Find(id);
            if (dualDatabaseTestSchemeTest == null)
            {
                return HttpNotFound();
            }

            //count questions
            ViewBag.countquestions = null;
            if(TempData["countquestions"] != null)
            {
                ViewBag.countquestions = (int)TempData["countquestions"];
            }




            return View(dualDatabaseTestSchemeTest);
        }

        // GET: DualDatabaseTestSchemeTests/Edit/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DualDatabaseTestSchemeTest dualDatabaseTestSchemeTest = db.DualDatabaseTestSchemeTestDataBase.Find(id);
            if (dualDatabaseTestSchemeTest == null)
            {
                return HttpNotFound();
            }
            return View(dualDatabaseTestSchemeTest);
        }

        // POST: DualDatabaseTestSchemeTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string Public, [Bind(Include = "Name,Description,Genre")] DualDatabaseTestSchemeTest dualDatabaseTestSchemeTest)
        {
            if (ModelState.IsValid)
            {
                dualDatabaseTestSchemeTest.Status = Public;
                dualDatabaseTestSchemeTest.DateModified = DateTime.Now.ToString();
                db.Entry(dualDatabaseTestSchemeTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PersonalProfile");
            }
            return View(dualDatabaseTestSchemeTest);
        }

        // GET: DualDatabaseTestSchemeTests/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DualDatabaseTestSchemeTest dualDatabaseTestSchemeTest = db.DualDatabaseTestSchemeTestDataBase.Find(id);
            if (dualDatabaseTestSchemeTest == null)
            {
                return HttpNotFound();
            }
            return View(dualDatabaseTestSchemeTest);
        }

        // POST: DualDatabaseTestSchemeTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            DualDatabaseTestSchemeTest dualDatabaseTestSchemeTest = db.DualDatabaseTestSchemeTestDataBase.Find(id);
            db.DualDatabaseTestSchemeTestDataBase.Remove(dualDatabaseTestSchemeTest);
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

        //new additions

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Public, [Bind(Include = "Name,Description,Genre")] DualDatabaseTestSchemeTest newTest)
        {

            //Random is no longer necessary
            //newTest.ID = random.Next(1, 50000);
            newTest.Owner = System.Web.HttpContext.Current.User.Identity.Name;
            newTest.DateModified = DateTime.Now.ToString();
            newTest.Status = Public;



            db.DualDatabaseTestSchemeTestDataBase.Add(newTest);
            db.SaveChanges();
            /*
            db.DualDatabaseTestSchemeTestDataBase.Attach(newTest);
            db.Entry(newTest).State = EntityState.Modified;
            db.SaveChanges();*/

            return RedirectToAction("Index");
        }

        public ActionResult CreateAdvanced()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdvanced(string Public, [Bind(Include = "Name,Description,Genre")] DualDatabaseTestSchemeTest newTest)
        {

            //Random is no longer necessary
            //newTest.ID = random.Next(1, 50000);
            newTest.Owner = System.Web.HttpContext.Current.User.Identity.Name;
            newTest.DateModified = DateTime.Now.ToString();
            newTest.Status = Public;
            newTest.IsAdvanced = true;



            db.DualDatabaseTestSchemeTestDataBase.Add(newTest);
            db.SaveChanges();
            /*
            db.DualDatabaseTestSchemeTestDataBase.Attach(newTest);
            db.Entry(newTest).State = EntityState.Modified;
            db.SaveChanges();*/

            return RedirectToAction("Index");
        }

        //legacy function
        public ActionResult CreateTestObject(string Public, [Bind(Include = "Name,Description,Genre")] DualDatabaseTestSchemeTest newTest)
        {

            //Random is no longer necessary
            //newTest.ID = random.Next(1, 50000);
            newTest.Owner = System.Web.HttpContext.Current.User.Identity.Name;
            newTest.DateModified = DateTime.Now.ToString();
            newTest.Status = Public;



            db.DualDatabaseTestSchemeTestDataBase.Add(newTest);
            db.SaveChanges();
            /*
            db.DualDatabaseTestSchemeTestDataBase.Attach(newTest);
            db.Entry(newTest).State = EntityState.Modified;
            db.SaveChanges();*/

            return RedirectToAction("Index");
        }


        public ActionResult AddNewQuestion(int? id)
        {
            ViewBag.ID = id;
            return View();
        }

        [AllowAnonymous]
        public ActionResult AllTests()
        {
            return View(db.DualDatabaseTestSchemeTestDataBase.ToList());
        }

        [AllowAnonymous]
        public ActionResult AllTestsPublic(string searchString = "")
        {
            var TestsSelected = from m in db.DualDatabaseTestSchemeTestDataBase select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                TestsSelected = TestsSelected.Where(s => s.Name.Contains(searchString));
            }

            //return View(db.DualDatabaseTestSchemeTestDataBase.ToList());
            return View(TestsSelected);
        }

        [AllowAnonymous]
        public ActionResult PersonalProfile()
        {
            return View(db.DualDatabaseTestSchemeTestDataBase.ToList());
        }


    }
}


