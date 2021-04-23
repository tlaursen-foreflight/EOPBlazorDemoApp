using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class JeppesenProcedure
    {
        public int ProcedureId { get; set; }
        public int RunwayId { get; set; }

        public virtual Runway Runway { get; set; }
    }
}
