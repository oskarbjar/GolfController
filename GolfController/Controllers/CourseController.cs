using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GolfController.Models;

namespace GolfController.Controllers
{ 
    public class CourseController : Controller
    {
        private HoleContext db = new HoleContext();

        //
        // GET: /Course/

        public ViewResult Index()
        {
            
            
            var totalLengthController = (from item in db.hole 
                              where item.CourseID == 1
                              select item.Par).Sum();

               Course.Foo = totalLengthController;
            return View(db.course.ToList());
        }

        //
        // GET: /Course/Details/5

        public ViewResult Details(int id)
        {
            Course course = db.course.Find(id);
            return View(course);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.course.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(course);
        }
        
        //
        // GET: /Course/Edit/5
 
        public ActionResult Edit(int id)
        {
            Course course = db.course.Find(id);
            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        //
        // GET: /Course/Delete/5
 
        public ActionResult Delete(int id)
        {
            Course course = db.course.Find(id);
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Course course = db.course.Find(id);
            db.course.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}