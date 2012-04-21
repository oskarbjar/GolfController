using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfController.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int  NumberOfHoles { get; set; }
        public int TotalLength { get; set; }
        public  int TotalPar { get; set; }
        public int MyProperty { get; set; }
        public virtual ICollection<Hole> holes { get; set; }


    }

    public class Hole
    {
        public int ID { get; set; }
        public string HoleNumber { get; set; }
        public int Length { get; set; }
        public int Par { get; set; }            
        public int CourseID { get; set; }
        public decimal AvgScore { get; set; }

        public Course course { get; set; }
    }

    public class ScoreCard
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public int Putts { get; set; }
        public bool FairwayInRegulation { get; set; }
        public bool GreenInRegulation { get; set; }
        public int HoleID { get; set; }
        public string Notes { get; set; }

        public Hole hole { get; set; }



    }
}