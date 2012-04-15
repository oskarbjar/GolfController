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

        public ViewResult Index()
        {
            var scorecard = db.scorecard.Include(s => s.hole);
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

        public ActionResult Create()
        {
            ViewBag.HoleID = new SelectList(db.hole, "ID", "HoleNumber");
            return View();
        } 

        //
        // POST: /ScoreCard/Create

        [HttpPost]
        public ActionResult Create(ScoreCard scorecard)
        {
            if (ModelState.IsValid)
            {
                db.scorecard.Add(scorecard);
                db.SaveChanges();
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
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}