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
        [Authorize(Roles = "canEdit")]
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
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit([Bind(Include = "ID,Name,Description")] DualDatabaseTestSchemeTest dualDatabaseTestSchemeTest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dualDatabaseTestSchemeTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dualDatabaseTestSchemeTest);
        }

        // GET: DualDatabaseTestSchemeTests/Delete/5
        [Authorize(Roles = "canEdit")]
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
        [Authorize(Roles = "canEdit")]
        public ActionResult Create()
        {

            return View();
        }

        [Authorize(Roles = "canEdit")]
        public ActionResult CreateTestObject(string Name, String Description)
        {
            //Random random = new Random();
            DualDatabaseTestSchemeTest newTest = new DualDatabaseTestSchemeTest();

            //Random is no longer necessary
            //newTest.ID = random.Next(1, 50000);
            newTest.Name = Name;
            newTest.Description = Description; //+ " ID: " + newTest.ID;



            db.DualDatabaseTestSchemeTestDataBase.Add(newTest);
            db.SaveChanges();
            /*
            db.DualDatabaseTestSchemeTestDataBase.Attach(newTest);
            db.Entry(newTest).State = EntityState.Modified;
            db.SaveChanges();*/

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "canEdit")]
        public ActionResult AddNewQuestion(int? id)
        {
            ViewBag.ID = id;
            return View();
        }

        public ActionResult AllTests()
        {
            return View(db.DualDatabaseTestSchemeTestDataBase.ToList());
        }

    }
}


