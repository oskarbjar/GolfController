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

        public ViewResult ViewHoles(int id)
        {
            var hole = db.hole.OrderBy(m=> m.HoleNumber).Where(m => m.CourseID == id);
                return View ( hole.ToList());
        }

       
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
                CalculatePar(hole);
                CalculateLength(hole);
                return RedirectToAction("ViewHoles", new { id = hole.CourseID });  
            }

            ViewBag.CourseID = new SelectList(db.course, "ID", "Name", hole.CourseID);
            return View(hole);
        }
        
        private void CalculatePar(Hole hole)
        {
            var totalPar = (from item in db.hole
                                         where item.CourseID == hole.CourseID
                                         select item.Par).Sum();
            var course = db.course.Find(hole.CourseID);
            course.TotalPar = totalPar;
            db.SaveChanges();
        }

        private void CalculateLength(Hole hole)
        {
            var totalLength = (from item in db.hole
                            where item.CourseID == hole.CourseID
                            select item.Length).Sum();
            var course = db.course.Find(hole.CourseID);
            course.TotalLength = totalLength;
            db.SaveChanges();

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
                CalculatePar(hole);
                CalculateLength(hole);
                return RedirectToAction("ViewHoles", new { id = hole.CourseID });
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
            CalculatePar(hole);
            return RedirectToAction("ViewHoles", new { id = hole.CourseID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}