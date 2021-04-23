using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class DbService: IDbService
    {
        private readonly DatabaseContext _context;

        public DbService(DatabaseContext context)
        {
            _context = context;
        }

        Task<List<EngineOutProcedure>> IDbService.GetEOPs()
        {
            return _context.EOPS.ToListAsync();
        }
    }
}