using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class DbService : IDbService
    {
        private readonly DatabaseContext _context;

        public DbService(DatabaseContext context)
        {
            _context = context;
        }

        public Task<List<EngineOutProcedure>> GetEops()
        {
            return _context.EngineOutProcedures.Include(eop => eop.Runway.Airport).ToListAsync();
        }

        public Task<List<Airport>> GetAirports()
        {
            return _context.Airports.ToListAsync();
        }

        public Task<List<Runway>> GetRunways()
        {
            return _context.Runways.ToListAsync();
        }
    }
}