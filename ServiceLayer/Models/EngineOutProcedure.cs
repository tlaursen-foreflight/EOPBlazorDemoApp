using System;

namespace ServiceLayer.Models
{
    public class EngineOutProcedure
    {
        public int ProcedureId { get; set; }

        public int RunwayId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string EOPClass { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string CreatorComments { get; set; }

        public DateTime ReviewedOn { get; set; }

        public string ReviewBy { get; set; }

    }
}