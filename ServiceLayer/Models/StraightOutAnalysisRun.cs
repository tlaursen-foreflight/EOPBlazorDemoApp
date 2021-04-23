using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class StraightOutAnalysisRun
    {
        public int StraightOutAnalysisRunId { get; set; }
        public int StraightOutId { get; set; }
        public int AiracCycle { get; set; }
        public string NavDbdataSet { get; set; }
        public DateTime AnalysedOn { get; set; }
        public string Results { get; set; }
        public string AnalysisVersion { get; set; }

        public virtual StraightOut StraightOut { get; set; }
    }
}
