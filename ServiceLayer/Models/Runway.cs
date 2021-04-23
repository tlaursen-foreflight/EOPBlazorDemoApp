using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class Runway
    {
        public Runway()
        {
            EngineOutProcedures = new HashSet<EngineOutProcedure>();
            JeppesenProcedures = new HashSet<JeppesenProcedure>();
            StraightOuts = new HashSet<StraightOut>();
        }

        public int RunwayId { get; set; }
        public int AirportId { get; set; }
        public string Name { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual ICollection<EngineOutProcedure> EngineOutProcedures { get; set; }
        public virtual ICollection<JeppesenProcedure> JeppesenProcedures { get; set; }
        public virtual ICollection<StraightOut> StraightOuts { get; set; }
    }
}
