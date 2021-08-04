using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class EngineOutProcedure
    {
        public EngineOutProcedure()
        {
            AnalysisRuns = new HashSet<AnalysisRun>();
        }

        
        public int ProcedureId { get; set; }
        public int RunwayId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Eopclass { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorComments { get; set; }
        public DateTime? ReviewedOn { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewComments { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public string Path { get; set; }
        public string SuppressedObstacles { get; set; }
        public double DistanceToAirportBoundaryFeet { get; set; }
        public bool HasChangesRequested { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime LastEditedOn { get; set; }

        public virtual Runway Runway { get; set; }
        public virtual ICollection<AnalysisRun> AnalysisRuns { get; set; }
    }
}
