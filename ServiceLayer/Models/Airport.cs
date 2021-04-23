using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class Airport
    {
        public Airport()
        {
            Runways = new HashSet<Runway>();
        }

        public int AirportId { get; set; }
        public string Icao { get; set; }
        public string Iata { get; set; }
        public string RegionalCode { get; set; }
        public string Fir { get; set; }

        public virtual ICollection<Runway> Runways { get; set; }
    }
}
