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
    public class HoleController : Controller
    {
        private HoleContext db = new HoleContext();

        //
        // GET: /Hole/

        public ViewResult Index()
        {
            var hole = db.hole.Include(h => h.course);
            return View(hole.ToList());
        }

        //
        // GET: /Hole/Details/5

        public ViewResult Details(int id)
        {
            Hole hole = db.hole.Find(id);
            return View(hole);
        }

        //
        // GET: /Hole/Create

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.course, "ID", "Name");
            return View();
        } 

        //
        // POST: /Hole/Create

        [HttpPost]
        public ActionResult Create(Hole hole)
        {
            if (ModelState.IsValid)
            {
                db.hole.Add(hole);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CourseID = new SelectList(db.course, "ID", "Name", hole.CourseID);
            return View(hole);
        }
        
        //
        // GET: /Hole/Edit/5
 
        public ActionResult Edit(int id)
        {
            Hole hole = db.hole.Find(id);
            ViewBag.CourseID = new SelectList(db.course, "ID", "Name", hole.CourseID);
            return View(hole);
        }

        //
        // POST: /Hole/Edit/5

        [HttpPost]
        public ActionResult Edit(Hole hole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.course, "ID", "Name", hole.CourseID);
            return View(hole);
        }

        //
        // GET: /Hole/Delete/5
 
        public ActionResult Delete(int id)
        {
            Hole hole = db.hole.Find(id);
            return View(hole);
        }

        //
        // POST: /Hole/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Hole hole = db.hole.Find(id);
            db.hole.Remove(hole);
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