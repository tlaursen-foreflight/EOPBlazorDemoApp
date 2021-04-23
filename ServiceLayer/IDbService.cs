using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public interface IDbService
    {
        public Task<List<EngineOutProcedure>> GetEOPs();
    }
}