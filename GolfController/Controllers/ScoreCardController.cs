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
    public class ScoreCardController : Controller
    {
        private HoleContext db = new HoleContext();

        //
        // GET: /ScoreCard/

        public ViewResult Index(int id)
        {
            //var scorecard = db.scorecard.Include(s => s.hole).Where(h => h.HoleID == id );
            var scorecard = db.scorecard.Where(h => h.HoleID == id);
            return View(scorecard.ToList());
        }

        //
        // GET: /ScoreCard/Details/5

        public ViewResult Details(int id)
        {
            ScoreCard scorecard = db.scorecard.Find(id);
            return View(scorecard);
        }

        //
        // GET: /ScoreCard/Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.HoleID = new SelectList(db.hole, "ID", "HoleNumber", id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Create");
            }
            ViewBag.HoleID = new SelectList(db.hole, "ID", "HoleNumber", id);
          
            return View();
        } 

        //
        // POST: /ScoreCard/Create

        [HttpPost]
        public ActionResult Create(ScoreCard scorecard)
        {
            // TODO: put in temp message that you need too refresh to see latest avg score 
            if (ModelState.IsValid)
            {
           
                db.scorecard.Add(scorecard);
                db.SaveChanges();
                CalculateAverageScore(scorecard.HoleID); 
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ThanksForFeedback");
                  // return RedirectToAction("Hole", "ViewHoles", new { id = scorecard.hole.CourseID });
                }
                return RedirectToAction("Index");  
            }

            ViewBag.HoleID = new SelectList(db.hole, "ID", "HoleNumber", scorecard.HoleID);
            return View(scorecard);
        }
        
        //
        // GET: /ScoreCard/Edit/5
 
        public ActionResult Edit(int id)
        {
            ScoreCard scorecard = db.scorecard.Find(id);
            ViewBag.HoleID = new SelectList(db.hole, "ID", "HoleNumber", scorecard.HoleID);
            return View(scorecard);
        }

        //
        // POST: /ScoreCard/Edit/5

        [HttpPost]
        public ActionResult Edit(ScoreCard scorecard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scorecard).State = EntityState.Modified;
                
                db.SaveChanges();
                CalculateAverageScore(scorecard.HoleID); 
                return RedirectToAction("Index");
            }
            ViewBag.HoleID = new SelectList(db.hole, "ID", "HoleNumber", scorecard.HoleID);
            return View(scorecard);
        }

        //
        // GET: /ScoreCard/Delete/5
 
        public ActionResult Delete(int id)
        {
            ScoreCard scorecard = db.scorecard.Find(id);
            return View(scorecard);
        }

        //
        // POST: /ScoreCard/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ScoreCard scorecard = db.scorecard.Find(id);
            db.scorecard.Remove(scorecard);
            db.SaveChanges();
            CalculateAverageScore(scorecard.HoleID);
            return RedirectToAction("Index", new { id = scorecard.HoleID });
        }

        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void CalculateAverageScore(int id)
        {                     
            var hole = db.hole.Find(id);
            var ifexists = db.scorecard.Any(m => m.HoleID == id);


            if (ifexists)
            {
                decimal number = db.scorecard.Where(m => m.HoleID == id).Count();
                var totalScore = (from item in db.scorecard
                                  where item.HoleID == id
                                  select item.Score).Sum();
                hole.AvgScore = totalScore / number;
                db.SaveChanges();

            }
            else
            {
                hole.AvgScore = 0;
                db.SaveChanges();
            
            }
                  

           

           





        }
    }
}