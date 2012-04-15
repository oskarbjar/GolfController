using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GolfController.Models
{
    public partial class HoleContext:DbContext
    {
        public HoleContext()
            : base("GolfDb")
        { 
        }
        
        public DbSet<Hole> hole { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<ScoreCard> scorecard { get; set; }

    }
}