using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class StraightOut
    {
        public StraightOut()
        {
            StraightOutAnalysisRuns = new HashSet<StraightOutAnalysisRun>();
        }

        public int StraightOutId { get; set; }
        public int RunwayId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string Path { get; set; }
        public double DistanceToAirportBoundaryFeet { get; set; }

        public virtual Runway Runway { get; set; }
        public virtual ICollection<StraightOutAnalysisRun> StraightOutAnalysisRuns { get; set; }
    }
}
