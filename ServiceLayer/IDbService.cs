using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public interface IDbService
    {
        public void SetDbContext(string username, string password);
        public Task<bool> CheckDbAccess();
        public Task<List<EngineOutProcedure>> GetEops();
        public Task<List<Airport>> GetAirports();
        public Task<List<Runway>> GetRunways();
    }
}