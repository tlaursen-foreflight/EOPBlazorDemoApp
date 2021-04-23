using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class AnalysisRun
    {
        public int AnalysisRunId { get; set; }
        public int ProcedureId { get; set; }
        public string NavDbdataSet { get; set; }
        public string ObstacleDbdataSet { get; set; }
        public string Results { get; set; }
        public string AnalysisVersion { get; set; }
        public int AiracCycle { get; set; }
        public DateTime AnalysedOn { get; set; }

        public virtual EngineOutProcedure Procedure { get; set; }
    }
}
