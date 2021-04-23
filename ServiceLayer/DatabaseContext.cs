using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
        }
        public DbSet<EngineOutProcedure> EOPS { get; set; }
    }
}