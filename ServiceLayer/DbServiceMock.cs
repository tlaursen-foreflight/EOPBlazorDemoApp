using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class DbServiceMock : IDbService
    {
        public Task<List<EngineOutProcedure>> GetEOPs()
        {
            return Task.FromResult(MockData());
        }


        private static List<EngineOutProcedure> MockData() => new List<EngineOutProcedure>
        {
            new EngineOutProcedure { Description = "asdasd" }
        };
    }
}